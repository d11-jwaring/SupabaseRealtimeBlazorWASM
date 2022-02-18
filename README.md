# SupabaseRealtimeBlazorWASM
Demonstrates a potential fix for using the realtime-csharp package in a Blazor WASM app

Run the docker-compose file with docker-compose up.

Add the 'realtime-csharp.2.0.8.nupkg' to your local nuget source. you may need to clear your nuget cache before installing as it wll use the old dependancies.

Add local realtime-csharp to the RealtimeCshar.Client project (if needed).

Run the project and look at the browser console log.
