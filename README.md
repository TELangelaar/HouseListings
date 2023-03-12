# Project Outline
- Backend:
  - [x] Asp.net core Web-Api
      - [x] Docker support
      - [] Authorization using Microsoft Identity Platform 
      - Deployed to Azure App Service 
      - Database:
        - [x] MSSQL
        - [x] Entity FrameWork Core
        - [x] To store house-listings
      - [x] Structured Logging (Sensilog)
	 - [x] EventIds
         - Application Insights (?)
      - Middleware
	 - Hellang problemdetails
      - Unit Tests
      - Feature Tests
      - Integration Tests
  - Signed-up users can view House Listings
  - Signed-in users can subscribe to House Listings
- Frontend:
  - Angular
  - Single Page where users can sign-up / sign-in
  - After sign-in, can request all House Listings
  - Can select one or more House Listings that they want to subscribe to
  - Can view their subscribed to House Listings
  - Retrieve notifications when a House Listing is updated