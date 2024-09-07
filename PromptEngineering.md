To design prompts that will guide the AI model to create and update USSD application menus, we need to clearly outline the steps and ensure that the AI understands the structure and fields required for each API call. Here’s how we can structure the prompts:

### Initial Menu Creation Prompt

This prompt should help the AI model understand how to create the initial menus.

**Prompt:**
```
You are an AI designed to help build USSD applications. The first step is to create the initial menus for the application. 
Each menu should have a display text, a user-defined name, and other fields. Here is the format for creating a menu:

API Endpoint: /api/addMenu
```
Payload:
```json
{
  appId: '{appId}',
  isFirst: {isFirst},
  displayText: '{displayText}',
  headerText: '{headerText}',
  footerText: '{footerText}',
  userDefinedName: '{userDefinedName}',
  inputHolder: '{userDefinedName}',
  allowGoBack: {allowGoBack},
  terminate: {terminate},
  goBackInputIndicator: '{goBackInputIndicator}',
  parentMenuId: '{parentMenuId}',
  switchOperations: [],
  actions: [],
  inputValidations: []
```

1. Create the main menu with the following display text:
   ```
   Welcome to ASOKORE MAMPONG USSD.
   Please select an option:
   1 Airtime Topup
   2 Transfer
   3 Bank to Wallet
   4 Balance Enquiry
   5 Account Statement
   6 Asokore Merchant Pay
   7 Self Services
   ```
   Use the user-defined name 'mainMenu'.

2. Create a submenu for each option in the main menu. Example for option 2 (Transfer):
   ```
   Transfer To Mobile
   Select Network
   1 MTN
   2 VODAFONE
   3 AIRTELTIGO
   ```

Provide the payloads for creating these menus.
```

### Update Menus with Navigation Prompt

This prompt will guide the AI model to update the menus with navigation details after all the menus have been created.

**Prompt:**
```
Now that all the menus have been created, the next step is to add navigation details. To do this, we need to fetch all menus and then update each menu with the correct navigation definitions.

1. Fetch all menus using the API:
   ```
   GET /api/getAllMenus
   ```
   This will return an array of menus with their unique IDs.

2. Update the main menu to navigate to the appropriate submenu based on the user's input. Here is the format for updating a menu:

API Endpoint: /api/updateMenu
Payload:
{
appId: '{appId}',
isFirst: {isFirst},
displayText: '{displayText}',
headerText: '{headerText}',
footerText: '{footerText}',
userDefinedName: '{userDefinedName}',
inputHolder: '{inputHolder}',
allowGoBack: {allowGoBack},
terminate: {terminate},
goBackInputIndicator: '{goBackInputIndicator}',
parentMenuId: '{parentMenuId}',
uniqueId: '{uniqueId}',
switchOperations: [
{
action: 'goto',
actionParam: '{submenuUniqueId1}',
operator: 'eq',
compareVal: '1'
},
{
action: 'goto',
actionParam: '{submenuUniqueId2}',
operator: 'eq',
compareVal: '2'
},
...
],
actions: [],
inputValidations: []
}

Provide the payloads to update the main menu and link it to the corresponding submenus based on the user's input.
```

### Combined Prompt

If you prefer a single prompt to handle both creation and updating of menus, you can combine the steps:

**Combined Prompt:**
```
You are an AI designed to help build USSD applications. The first step is to create the initial menus, and then update these menus with navigation details.

1. Create the main menu with the following display text:
   ```
   Welcome to ASOKORE MAMPONG USSD.
   Please select an option:
   1 Airtime Topup
   2 Transfer
   3 Bank to Wallet
   4 Balance Enquiry
   5 Account Statement
   6 Asokore Merchant Pay
   7 Self Services
   ```
   Use the user-defined name 'mainMenu'. Here is the payload for creating this menu:
   ```
   {
     appId: '{appId}',
     isFirst: true,
     displayText: 'Welcome to ASOKORE MAMPONG USSD.\nPlease select an option:\n1 Airtime Topup\n2 Transfer\n3 Bank to Wallet\n4 Balance Enquiry\n5 Account Statement\n6 Asokore Merchant Pay\n7 Self Services',
     headerText: '',
     footerText: '',
     userDefinedName: 'mainMenu',
     inputHolder: 'mainMenu',
     allowGoBack: true,
     terminate: false,
     goBackInputIndicator: '0',
     parentMenuId: '',
     switchOperations: [],
     actions: [],
     inputValidations: []
   }
   ```

2. Create submenus for each option in the main menu. Example for option 2 (Transfer):
   ```
   Transfer To Mobile
   Select Network
   1 MTN
   2 VODAFONE
   3 AIRTELTIGO
   ```
   Provide the payloads for creating these submenus.

3. Fetch all menus using the API:
   ```
   GET /api/getAllMenus
   ```
   This will return an array of menus with their unique IDs.



4. Update the main menu to navigate to the appropriate submenu based on the user's input. Here is the format for updating the main menu:
   ```
   {
     appId: '{appId}',
     isFirst: true,
     displayText: 'Welcome to ASOKORE MAMPONG USSD.\nPlease select an option:\n1 Airtime Topup\n2 Transfer\n3 Bank to Wallet\n4 Balance Enquiry\n5 Account Statement\n6 Asokore Merchant Pay\n7 Self Services',
     headerText: '',
     footerText: '',
     userDefinedName: 'mainMenu',
     inputHolder: 'mainMenu',
     allowGoBack: true,
     terminate: false,
     goBackInputIndicator: '0',
     parentMenuId: '{parentMenuId}',
     uniqueId: '{mainMenuUniqueId}',
     switchOperations: [
       {
         action: 'goto',
         actionParam: '{submenuUniqueId1}',
         operator: 'eq',
         compareVal: '1'
       },
       {
         action: 'goto',
         actionParam: '{submenuUniqueId2}',
         operator: 'eq',
         compareVal: '2'
       },
       ...
     ],
     actions: [],
     inputValidations: []
   }
   ```
   Provide the payload to update the main menu with the navigation details.
```

These prompts should help guide the AI model in creating and updating the USSD application menus. Adjust the `{placeholders}` with actual values as needed.