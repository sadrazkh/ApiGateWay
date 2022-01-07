using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Services.WebFramework.Pagination
{
    public static class PaginationExtensions
    {
        public static void AddAppError(this HttpResponse response, string message)
        {
            response.Headers.Add("App-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "App-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static void AddPaginationHeader(this HttpResponse response, int totalPages, int itemsPerPage, int currentPage, int totalItems)
        {
            var paginationHeader = new PaginationHeader(totalPages, itemsPerPage, currentPage, totalItems);
            var camelCaseFormater = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormater));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");

        }

        public static IQueryable<T> SkipAndTake<T>(this IQueryable<T> query, PaginationParameters parameters)
        {
            return query.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize);
        }
    }
}
