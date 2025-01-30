The project is set up to run locally using Docker compose. A Docker container for MongoDB is included, and everything runs in containers for easy testing.

Steps for Testing:
Clone the Repository:

Ensure you have the repository cloned to your local machine.
Docker Setup:

Run the project using Docker compose, which will automatically spin up the necessary containers, including MongoDB.
Configuration Files:

I’ve removed all sensitive tokens and keys for security reasons. You will need to provide your own credentials:
JWT Configuration: Generate your own JWT secret key and update the Jwt.Key.
OpenAI Configuration: Obtain your OpenAI API key and replace the value in OpenAISettings.ApiKey.
Twilio Configuration: If testing the WhatsApp integration, set up your own Twilio account and replace the necessary keys.
Video Walkthrough:

I’ve created a video walkthrough showing how the project works. You can view it here: Project Walkthrough Video.
Once you’ve set up your credentials and run the Docker containers, you should be able to test the project and see how it works.

Let me know if you encounter any issues or need further assistance!

https://www.loom.com/share/dce03f89bbcf4a4383ba31487e11a2ac
