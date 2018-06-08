# Create the build environment image
FROM microsoft/dotnet:2.0-sdk as build-env
WORKDIR /app


# Copy the project files
COPY /SharedModels/*.csproj SharedModels/
COPY /VodacommessagingXml2sms/*.csproj VodacommessagingXml2sms/


# Restore all the dependencies
WORKDIR /app/SharedModels
RUN dotnet restore
WORKDIR /app/VodacommessagingXml2sms
RUN dotnet restore


# Now copy all the remaining files
WORKDIR /app
COPY SharedModels/*.* SharedModels/
COPY VodacommessagingXml2sms/*.* VodacommessagingXml2sms/


# Publish the app
WORKDIR /app/VodacommessagingXml2sms
RUN dotnet publish -c Release -o out


# Build the runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "VodacommessagingXml2sms.dll"]
