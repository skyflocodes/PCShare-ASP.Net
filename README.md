# ASPFinalProject
A website that will take the parts of a users submitted computer and will break down pricing, save links to buy each part as well as specs, there can also be a pc of the day page where an admin can choose a submitted pc to display on the front page of the website.

LIVE SITE:
https://pcshare20201019021452-pcshare.azurewebsites.net/

I had many issues with an error stating
"Error.
An error occurred while processing your request.
Request ID: |4f62e185-408e6f499a138e30.

Development Mode
Swapping to Development environment will display more detailed information about the error that occurred.

The Development environment shouldn't be enabled for deployed applications. It can result in displaying sensitive information from exceptions to end users. 
For local debugging, enable the Development environment by setting the ASPNETCORE_ENVIRONMENT environment variable to Development and restarting the app."

I couldnt find any information regarding this other than to "setting the ASPNETCORE_ENVIRONMENT environment variable to Development" but when I opened 
launchSettings.json it was already set to development mode, I also looked through many websites to try and find a way to set this whithin azure and nothing seemed to help.
would love to hear a way to fix this (or maybe it is supposed to act this way?) Thanks, Sky.
