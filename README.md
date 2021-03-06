# IceWarp.Net
A .net wrapper for the IceWarp API

IceWarp.Net fully implements the [IceWarp API RPC](https://www.icewarp.co.uk/api/) calls.

## NuGet

```bash
PM> Install-Package IceWarp.Net
```

## Usage
	string apiUrl = "http://localhost/icewarpapi/";
	var api = new IceWarpRpcApi();
	Authenticate authenticate = new Authenticate
	{
		AuthType = TAuthType.Plain,
		Digest = "",
		Email = "admin@email.com",
		Password = "password",
		PersistentLogin = false
	};
	SuccessResponse authResult = api.Execute(apiUrl, authenticate);
	
	GetSessionInfo sessionInfo = new GetSessionInfo
	{
		SessionId = authResult.SessionId
	};
	TAPISessionInfoResponse sessionInfoResult = api.Execute(apiUrl, sessionInfo);
	
	Logout logout = new Logout
	{
		SessionId = authResult.SessionId
	};
	SuccessResponse logoutResult = api.Execute(apiUrl, logout);

## Com objects

Get Properties requests can be converted into objects similar to the Com Objects installed on the server.

[IceWarp ServerAPI Reference](http://dl.icewarp.com/documentation/server/API/V%2011%20IceWarp%20Server%20API%20Reference.pdf).

### Domain Properties

Create a Domain object from a GetDomainProperties request.

	GetDomainProperties getPropertiesRequest = new GetDomainProperties
	{
		SessionId = "session id",
                DomainStr = "testing.co.uk",
                DomainPropertyList = new TDomainPropertyList
                {
                    Items = ClassHelper.PublicProperites(typeof(Domain)).Select(x => x.Name).ToList()
                }
	};
	TPropertyValueListResponse getPropertiesResult = api.Execute(apiUrl, getPropertiesRequest);
	Domain domain = new Domain(getPropertiesResult.Items);
	
Create a SetDomainProperties request from a Domain object.

	Domain domain = new Domain
	{
		D_Description = "description",
		D_AdminEmail = "admin@email.com
	};
	List<TPropertyValue> propertyValues = domain.BuildTPropertyValues();
	SetDomainProperties setPropertiesRequest = new SetDomainProperties
	{
		SessionId = "session id",
                DomainStr = "testing.co.uk",
                PropertyValueList = new TPropertyValueList
                {
                    Items = propertyValues	
                }
	};
	
Create a SetDomainProperties request from specific properties of a Domain object.

	Domain domain = new Domain
	{
		D_Description = "description",
		D_AdminEmail = "admin@email.com
	};
	List<TPropertyValue> propertyValues = domain.BuildTPropertyValues(new List<string> { "D_Description" });
	SetDomainProperties setPropertiesRequest = new SetDomainProperties
	{
		SessionId = "session id",
                DomainStr = "testing.co.uk",
                PropertyValueList = new TPropertyValueList
                {
                    Items = propertyValues	
                }
	};

### Account Properties

### Server Properties

