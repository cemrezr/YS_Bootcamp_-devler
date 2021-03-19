using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareL.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke (HttpContext context)
        {
            //First, get the incoming request
            var request = await FormatRequest(context.Request);

            //copy a pointer to the priginal response body stream
            var originalBodyStream = context.Response.Body;

            //create a new memory stream
            using(var responseBody = new MemoryStream())
            {
                //and use that for the temporary response body
                context.Response.Body = responseBody;

                //continue down the middleware pipeline, eventually returning to this class

                await _next(context);

                //format the response from the server
                var response = await FormatResponse(context.Response);

                //TODO: Save log to chosen datastore

                //copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.

            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, etc.)

            return $"{response.StatusCode} : {text}";
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            //This line allows us to set the reader for the request back at the beginning of its stream

            request.EnableBuffering();

            //We now need to read the request stream. First, we create a new byte[] with the same length as the request stream...

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //Then we copy the entire request stream into the new buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //we convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            request.Body = body;

            return $"{request.Scheme} {request.Host} {request.Path} {request.QueryString} {bodyAsText}";


        }


    }
}
