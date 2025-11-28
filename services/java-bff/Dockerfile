FROM eclipse-temurin:17-jdk-alpine

WORKDIR /app

ARG JAR_FILE=target/demo-0.0.1-SNAPSHOT.jar
COPY ${JAR_FILE} app.jar

# Perfil para usar application-docker.yml
ENV SPRING_PROFILES_ACTIVE=docker

# Puerto interno de la app Spring Boot
EXPOSE 8080

ENTRYPOINT ["java","-jar","/app/app.jar"]


