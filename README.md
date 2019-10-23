# DevArena2019
## To the Cloud and back, by Azure
[![Build Status](https://dev.azure.com/pstrucic/devarena-teams-integration/_apis/build/status/petrastrucic.DevArena2019?branchName=develop)](https://dev.azure.com/pstrucic/devarena-teams-integration/_build/latest?definitionId=3&branchName=develop)

SendEmail is an Azure function that is triggered by a new message in Service Bus. It processes the message and propagates it to SendGrid which is an another Azure resource. It sends email.
