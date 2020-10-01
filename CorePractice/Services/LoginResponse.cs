using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePractice.Services
{
    public class LoginResponse
    {
        public LoginResponse(string json)
        {
            if (json != null)
            {
                JObject str = JObject.Parse(json);

                var result = str["result"].ToString();
                var com = str["companies"].ToString();
                JObject str1 = JObject.Parse(result);


                ResponseObject Response = JsonConvert.DeserializeObject<ResponseObject>(json);
                var company = JsonConvert.DeserializeObject<List<Company>>(com);
                Success = (bool)str1["succeeded"];
                IsLockedOut = (bool)str1["isLockedOut"];
                IsNotAllowed = (bool)str1["isNotAllowed"];
                RequiresTwoFactor = (bool)str1["requiresTwoFactor"];
                Companies = company;
                UserId = Response.UserId;
                UserName = Response.UserName;
                Products = Response.Products;

            }
        }
        //{"id":0,"result":{"succeeded":true,"isLockedOut":false,"isNotAllowed":false,"requiresTwoFactor":false},
        //"userId":"508f84d3-0b1f-4ee8-af78-51f9959cf713",
        //"company":{"comId":"dapa26-414a-44e4-a287-18e846b51d99","companyName":"DAP"},
        //"products":[{"productId":4035,"versionId":3,"versionName":"Enterprise"}]}

        //{"id":0,
        //"result":"succeeded":true,"isLockedOut":false,"isNotAllowed":false,"requiresTwoFactor":false},
        //"userId":"17f1c267-86fe-4305-9a85-30b0ba1fbb85",
        //"companies":[{"comId":"e6a7e5b4-9586-4bed-bcb4-1149d9875425","companyName":"Lion Matrix"}],
        //"products":[{"productId":2018,"versionId":3,"versionName":"Enterprise"},{"productId":2018,"versionId":3,"versionName":"Enterprise"},{"productId":2018,"versionId":3,"versionName":"Enterprise"}]}


        public bool Success { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public bool RequiresTwoFactor { get; set; }


        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<Company> Companies { get; set; }
        public List<Product> Products { get; set; }


    }

    public class ResponseObject
    {
        public int Id { get; set; }
        public Microsoft.AspNetCore.Identity.SignInResult Result { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public Company Companie { get; set; }
        public List<Product> Products { get; set; }

    }
    public class Company
    {
        public Guid ComId { get; set; }
        public string CompanyName { get; set; }

        public static explicit operator Company(List<JToken> v)
        {
            throw new NotImplementedException();
        }
    }
    public class Product
    {
        public Guid ProductId { get; set; }
        public int VersionId { get; set; }
        public string VersionName { get; set; }
    }
}
