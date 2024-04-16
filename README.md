# Instagram Hashtag Preview

Instagram Hashtag Preview employs Puppeteer to automatically extract image URLs for specific Instagram hashtags. No
authentication or developer app needed. With this tool you can easily show a preview of Instagram images for a specific
hashtag on your website. This is very suitable for event websites, where you want to show images tagged with a specific
hashtag.

# How to Run

## Prerequisites

Before running the instagram-hashtag-preview, ensure you have Docker installed on your machine. If Docker is not
installed, you can download it from [Docker's official site](https://www.docker.com/get-started).

## Running the Application

### Start the Application

Use Docker Compose to start the application. Open a terminal and run the following command from the root directory of
the project:

```bash
docker-compose up -d
```

Accessing the Application:
With the services running, you can now access the application using curl or any HTTP client. To retrieve image URLs for
the Instagram hashtag #test, use the following curl command:

```bash
curl http://localhost:5101/photos/test
```

This will output the list of image URLs tagged with #test on Instagram.

### Stopping the Application

To stop and remove all the running services, use the following Docker Compose command:

```bash
docker-compose down
```

