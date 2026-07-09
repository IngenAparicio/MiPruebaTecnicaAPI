using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiPruebaTecnicaAccess.utilities
{
    public class Mapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            TDestination destination = new();

            var sourceProperties = typeof(TSource)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var destinationProperties = typeof(TDestination)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProp in sourceProperties)
            {
                var destProp = destinationProperties
                    .FirstOrDefault(p =>
                        p.Name == sourceProp.Name &&
                        p.PropertyType.IsAssignableFrom(sourceProp.PropertyType) &&
                        p.CanWrite);

                if (destProp != null)
                {
                    var value = sourceProp.GetValue(source);
                    destProp.SetValue(destination, value);
                }
            }

            return destination;
        }

        // Mapea una colección de TSource a una lista de TDestination
        public static List<TDestination> MapList<TSource, TDestination>(List<TSource>? sources)
            where TDestination : new()
        {
            var result = new List<TDestination>();

            if (sources == null)
                return result;

            foreach (var item in sources)
            {
                if (item == null)
                    continue;

                var mapped = Map<TSource, TDestination>(item);
                result.Add(mapped);
            }

            return result;
        }

        // Sobrecarga que acepta tipos List<T> en los parámetros genéricos, por ejemplo:
        // Mapper.MapList<List<User>, List<UserDTO>>(usersList)
        //public static TListDest MapList<TListSource, TListDest>(TListSource sources)
        //    where TListSource : class
        //    where TListDest : class
        //{
        //    if (sources == null)
        //        return Activator.CreateInstance<TListDest>();

        //    var sourceType = typeof(TListSource);
        //    var destType = typeof(TListDest);

        //    // Sólo soportamos List<T> como tipos genéricos aquí
        //    if (!sourceType.IsGenericType || !destType.IsGenericType)
        //        throw new InvalidOperationException("MapList<TListSource, TListDest> expects generic List<> types.");

        //    var sourceGenDef = sourceType.GetGenericTypeDefinition();
        //    var destGenDef = destType.GetGenericTypeDefinition();

        //    if (sourceGenDef != typeof(List<>) || destGenDef != typeof(List<>))
        //        throw new InvalidOperationException("MapList<TListSource, TListDest> expects List<> generic types.");

        //    var sourceElemType = sourceType.GetGenericArguments()[0];
        //    var destElemType = destType.GetGenericArguments()[0];

        //    var resultListType = typeof(List<>).MakeGenericType(destElemType);
        //    var resultList = (System.Collections.IList)Activator.CreateInstance(resultListType)!;

        //    var enumerable = sources as System.Collections.IEnumerable;
        //    if (enumerable == null)
        //        return (TListDest)Activator.CreateInstance(resultListType)!;

        //    // Buscar el método Map<TSource,TDestination>(TSource source)
        //    var mapMethod = typeof(Mapper).GetMethods(BindingFlags.Public | BindingFlags.Static)
        //        .FirstOrDefault(m => m.IsGenericMethodDefinition && m.Name == nameof(Map) && m.GetParameters().Length == 1);

        //    if (mapMethod == null)
        //        throw new InvalidOperationException("No se encontró el método Map genérico.");

        //    foreach (var item in enumerable)
        //    {
        //        if (item == null)
        //            continue;

        //        // Crear la versión genérica Map<sourceElemType,destElemType>
        //        var genericMap = mapMethod.MakeGenericMethod(sourceElemType, destElemType);
        //        var mapped = genericMap.Invoke(null, new object[] { item });
        //        resultList.Add(mapped!);
        //    }

        //    return (TListDest)resultList;
        //}

        public static TListDest MapList<TListSource, TListDest>(TListSource sources)
            where TListSource : System.Collections.IEnumerable
            where TListDest : class
        {
            if (sources == null)
                return Activator.CreateInstance<TListDest>();

            var sourceType = typeof(TListSource);
            var destType = typeof(TListDest);

            if (!sourceType.IsGenericType || !destType.IsGenericType)
                throw new InvalidOperationException("Los tipos deben ser List<>");

            var sourceElementType = sourceType.GetGenericArguments()[0];
            var destElementType = destType.GetGenericArguments()[0];

            var result =
                (System.Collections.IList)Activator.CreateInstance(
                    typeof(List<>).MakeGenericType(destElementType)
                )!;

            var mapMethod = typeof(Mapper)
                .GetMethod(nameof(Map))!
                .MakeGenericMethod(sourceElementType, destElementType);

            foreach (var item in sources)
            {
                if (item == null)
                    continue;

                result.Add(mapMethod.Invoke(null, new[] { item })!);
            }

            return (TListDest)result;
        }


    }
}
