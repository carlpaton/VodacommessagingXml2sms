# Create the build environment image
FROM microsoft/dotnet:2.0-sdk as build-env
WORKDIR /app


# Copy the project file and restore the dependencies
COPY /VodacommessagingXml2sms/*.csproj ./
COPY /SharedModels/*.csproj ./
RUN dotnet restore


# Copy the remaining source files and build the application
COPY . ./
RUN dotnet publish -c Release -o out


# Build the runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "VodacommessagingXml2sms.dll"]
