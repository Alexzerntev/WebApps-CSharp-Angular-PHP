using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LuckyWheel.Model
{
    public class Result<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }

        public static Result<T> CreateFailed(
            HttpStatusCode errorCode, string errorText)
        {
            return new Result<T>()
            {
                ErrorCode = (int)errorCode,
                ErrorText = errorText
            };
        }

        public static Result<T> CreateSuccessful(T data)
        {
            return new Result<T>()
            {
                ErrorCode = (int)HttpStatusCode.OK,
                Data = data
            };
        }

        public bool IsSuccess()
        {
            return ErrorCode == (int)HttpStatusCode.OK;
        }

        public IActionResult ToErrorResponse()
        {
            return new ContentResult
            {
                Content = ErrorText,
                StatusCode = ErrorCode,
            };
        }
    }
}
