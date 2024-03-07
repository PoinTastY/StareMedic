# StareMedic
HospitalManagementSoftware

This SW began with the request from a hospital in my hometown where they have a program older than me, so I decided to make one updated as a challenge for me and once i'm satisfacted with the result, i'll offer it to the hospital.

## What does it has?
> It is pretty simple, but its growing, comming soon: implement Compaqi SDK

1. Starting from the bottom, it has a database runing in postgres, and for the db context, we go with EntityFramework and Npsql.
2. Then i have a repositorie wich i use to comunicate the code-behind with the db, that basically just add functions to the db context list-like objects.
3. Finally, the MAUI mess, I rlly like this framework, and i do my best to keep the solution with a good shape, trying to follow the best practicies, and not to do a horrible UI.


The first release is almost here, after it, we will focus on implementing the CompaqSdk, for it to automatize the developing, also will be implementing automation tests with appium
