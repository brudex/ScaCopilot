ShortCodAfricaPayload
```json
  [
  {
    "parentMenuId": "b3fbc44a-b06c-4603-830e-389d24ad61f8",
    "uniqueId": "0cc4be1c-af64-4c4a-8470-8e647d4d0079",
    "menuText": "Welcome to ASOKORE MAMPONG USSD.\nPlease select an option:\n1 Airtime Topup\n2 Transfer\n3 Bank to Wallet\n4 Balance Enquiry\n5 Account Statement\n6 Asokore Merchant Pay\n7 Self Services",
    "shortText": "Welcome to ASOKORE MAMPONG USSD.\nPlease select an option:\n1 Airtime Topup\n2 Transfer\n3 Bank to ...",
    "isFirst": true,
    "displayText": "Welcome to ASOKORE MAMPONG USSD.\nPlease select an option:\n1 Airtime Topup\n2 Transfer\n3 Bank to Wallet\n4 Balance Enquiry\n5 Account Statement\n6 Asokore Merchant Pay\n7 Self Services",
    "headerText": "",
    "footerText": "",
    "userDefinedName": "mainMenu",
    "inputHolder": "mainMenu",
    "allowGoBack": true,
    "terminate": false,
    "goBackInputIndicator": "0",
    "switchOperations": [
      {
        "action": "goto",
        "actionParam": "58006e20-227f-4f49-933f-b7564a5653b5",
        "operator": "eq",
        "compareVal": "2"
      },
      {
        "action": "goto",
        "actionParam": "6494dea6-3705-46ba-92bb-d47ccd3f7a31",
        "operator": "eq",
        "compareVal": "3"
      },
      {
        "action": "goto",
        "actionParam": "94154b00-8e4e-4b08-a5d1-3c8acd42c38d",
        "operator": "eq",
        "compareVal": "4"
      }
    ],
    "inputValidations": []
  },
  {
    "parentMenuId": "0cc4be1c-af64-4c4a-8470-8e647d4d0079",
    "uniqueId": "6dad0ec0-1acf-4e71-89e8-c45363ae7366",
    "menuText": "Transfer To Mobile\nSelect Network\n1 MTN\n2 VODAFONE\n3 AIRTELTIGO {{ session.mobile }} {{ session.network }}",
    "shortText": "Transfer To Mobile\nSelect Network\n1 MTN\n2 VODAFONE\n3 AIRTELTIGO {{ session.mobile }} {{ session...",
    "isFirst": false,
    "displayText": "Transfer To Mobile\nSelect Network\n1 MTN\n2 VODAFONE\n3 AIRTELTIGO {{ session.mobile }} {{ session.network }}",
    "headerText": "",
    "footerText": "",
    "userDefinedName": "confirmAmount",
    "inputHolder": "confirmAmount",
    "allowGoBack": true,
    "terminate": false,
    "goBackInputIndicator": "0",
    "switchOperations": [
      {
        "active": true,
        "action": "goto",
        "operator": "always",
        "actionParam": "8a675890-5d77-4c74-a4f6-5e8603e2bf43"
      }
    ],
    "inputValidations": [],
    "actions": []
  },
  {
    "parentMenuId": "6dad0ec0-1acf-4e71-89e8-c45363ae7366",
    "uniqueId": "8a675890-5d77-4c74-a4f6-5e8603e2bf43",
    "menuText": "Enter Amount  {{session.inputs.confirmAmount}}\n",
    "shortText": "Enter Amount  {{session.inputs.confirmAmount}}\n",
    "isFirst": false,
    "displayText": "Enter Amount  {{session.inputs.confirmAmount}}\n",
    "headerText": "",
    "footerText": "",
    "userDefinedName": "directOption",
    "inputHolder": "directOption",
    "allowGoBack": true,
    "terminate": false,
    "goBackInputIndicator": "0",
    "switchOperations": [],
    "inputValidations": [],
    "actions": []
  },
  {
    "parentMenuId": "8a675890-5d77-4c74-a4f6-5e8603e2bf43",
    "uniqueId": "7a8cf819-a0b5-46df-8592-16e3b402654f",
    "menuText": "Request received. You will receive a notification after processing\n",
    "shortText": "Request received. You will receive a notification after processing\n",
    "isFirst": false,
    "displayText": "",
    "headerText": "Request received. You will receive a notification after processing",
    "footerText": "",
    "userDefinedName": "directOption",
    "inputHolder": "directOption",
    "allowGoBack": true,
    "terminate": true,
    "goBackInputIndicator": "0",
    "switchOperations": [],
    "inputValidations": [],
    "actions": []
  }
]
```