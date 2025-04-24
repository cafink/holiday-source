FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

COPY . .

RUN dotnet tool install --global dotnet-aspnet-codegenerator

ENV PATH="$PATH:/root/.dotnet/tools"
ENV DOTNET_WATCH_RESTART_ON_RUDE_EDIT=true

EXPOSE 5254

CMD ["dotnet", "run"]