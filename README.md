### City Info APO
Publicly avail API. 
This is an upgrade project from ASP.NET API Core 2.2 to dotnet 9 WebApi.
This is also an API I want to repurpose to adopt xUnit learning.
xUnit recently released xUnit v3 and I want to use this opportunity to exlore this version + AutoFixture Library and explore the usage of FakeItEasy library.

## Notes:
First thing, first. 
Update to the solution as API went offline due to Aurelia Subscription ended. 

I'm changing this to using proper services and DTOs to be able to fully take advantage of testing capabilities of AutoFixture/FakeItEasy.

Removed in-memory store and now using EF 9. I have two DataSets, one for Cities and one for Point of Interests. 
