# Create the build environment image
FROM microsoft/aspnetcore:2.0
WORKDIR /app


# Copy the project file and restore the dependencies
COPY /VodacommessagingXml2sms/*.csproj ./
RUN dotnet restore


# Copy the remaining source files and build the application
COPY . ./
RUN dotnet publish -c Release -o out


# Build the runtime image
FROM microsoft/dotnet:1.1-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "VodacommessagingXml2sms.dll"]
