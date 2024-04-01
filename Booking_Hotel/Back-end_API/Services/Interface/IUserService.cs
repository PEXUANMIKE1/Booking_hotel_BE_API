﻿using Back_end_API.Entites;
using Back_end_API.Payload.DataRequests;
using Back_end_API.Payload.DataResponses;
using Back_end_API.Payload.Response;

namespace Back_end_API.Services.Interface
{
    public interface IUserService
    {
        ResponseObject<DataResponse_User> Register(Request_Register request);
        DataResponse_Token GenerateAccessToken(User user);
        ResponseObject<DataResponse_Token> Login (Request_Login request);
        DataResponse_Token RenewAccessToken (Request_RenewAccessToken request);
        IQueryable<DataResponse_User> GetAll();
    }
}