# AWSS3Service Class

## Overview
The `AWSS3Service` class offers a comprehensive suite of methods to interact with Amazon S3:
- Seamlessly create new S3 buckets.
- Effortlessly list existing S3 buckets.
- Easily upload files to S3.
- Conveniently download files from S3.

This service encapsulates the complexities of S3 operations, providing a user-friendly interface for managing your cloud storage needs.


## Getting Started

### Prerequisites
- .NET 5.0 or later
- AWS SDK for .NET
- AWS credentials with appropriate permissions

### Installation
1. Clone the repository:
    ```sh
    git clone https://github.com/davinder001/Demo_WebAPI_Access_AWSResources
    ```
2. Navigate to the project directory:
    ```sh
    cd Demo_WebAPI_Access_AWSResources
    ```
3. Restore the project dependencies:
    ```sh
    dotnet restore
    ```

## Configuration
Make sure to configure your AWS credentials and region in your `appsettings.development.json` file:

```json
{
    "AWS": {
        "AccessKey": "your-access-key",
        "SecretKey": "your-secret-key",
        "Region": "your-region"
    }
}
