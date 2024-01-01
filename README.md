# Apple Coupon API Microservice with .NET Core 8 , Service Bus , JWT Auth ... 
## Execute Docker Command to spin up ELK:
  ## docker-compose up -d

To test they are running we visit the following URLs

 ### ELS: http://localhost:9200
 ### Kibana: http://localhost:5601  

# Install the packages to configure ELK and Serilog
   ### dotnet add package Serilog.AspNetCore
   ### dotnet add package Serilog.Enrichers.Environment
   ### dotnet add package Serilog.Sinks.Debug
   ### dotnet add package Serilog.Sinks.Elasticsearch
   ### dotnet add package Serilog.Exceptions
   ### dotnet restore
