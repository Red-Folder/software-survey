param environment string = 'e2e'

var nameprefix = 'rfc-${environment}-software-survey'

resource signalr 'Microsoft.SignalRService/SignalR@2020-07-01-preview' = {
    name: '${nameprefix}-signalr'
    location: resourceGroup().location
    kind: 'SignalR'
    sku: {
        name: 'Free_F1'
    }
}

resource cosmos 'Microsoft.DocumentDB/databaseAccounts@2020-06-01-preview' = {
    name: '${nameprefix}-db'
    location: resourceGroup().location
    kind: 'GlobalDocumentDB'
    properties: {
        enableFreeTier: true
        databaseAccountOfferType: 'Standard'
        consistencyPolicy: {
            defaultConsistencyLevel: 'Session'
        }
    }
}

resource applicationInsights 'microsoft.insights/components@2018-05-01-preview' = {
    name: '${nameprefix}-application-insights'
    location: resourceGroup().location
    kind: 'web'
    properties: {
        ApplicationType: 'web'
        publicNetworkAccessForIngestion: 'Enabled'
        publicNetworkAccessForQuery: 'Enabled'
    }
}

resource farm 'Microsoft.Web/serverfarms@2018-11-01' = {
    name: '${nameprefix}-app-plan'
    location: resourceGroup().location
    kind: 'linux'
    sku: {
        name: 'F1'
        capacity: 1
    }
}

resource website 'Microsoft.Web/sites@2018-11-01' = {
    name: '${nameprefix}-web-app'
    location: resourceGroup().location
    kind: 'app'
    properties: {
        serverFarmId: farm.id
        siteConfig: {
            appSettings: [
                {
                    name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
                    value: reference(applicationInsights.id).InstrumentationKey
                }
                {
                    name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
                    value: reference(applicationInsights.id).ConnectionString
                }
                {
                    name: 'ApplicationInsightsAgent_EXTENSION_VERSION'
                    value: '~2'
                }
                {
                    name: 'Azure__SignalR__ConnectionString'
                    value: listKeys(signalr.id, '2020-07-01-preview').primaryConnectionString
                }
                {
                    name: 'Persistance:CosmosDbEndpoint'
                    value: cosmos.properties.documentEndpoint
                }
                {
                    name: 'Persistance:CosmosDbPrimaryKey'
                    value: listKeys(cosmos.id, '2020-06-01-preview').primaryMasterKey
                }
                {
                    name: 'XDT_MicrosoftApplicationInsights_Mode'
                    value: 'default'
                }
            ]
        }
    }
}

output appServiceName string = website.name