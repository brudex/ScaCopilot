
### ShortCodeAfricaJavascript

```javascript


var shortCode = this.session.shortCode;
var unDef = this.session["undefined"];
this.logger.info("The shorCode is >>",shortCode);
this.logger.info("hte session",this.session);
this.session.setSessionValue('payCode',shortCode);
this.session.setSessionValue('undefCode',unDef);



var apiResponse = this.actionResults['getTransactionDetails'];
var responseStatus = apiResponse.status;
if(responseStatus=='00'){
   this.session.setSessionValue('payAmount',apiResponse.amount);
   this.session.setSessionValue('payDescription',apiResponse.description);
}else{
    this.goto("invalidPaymentCode")
}






function extractLastSegment(str) {
  if (str.endsWith('#')) {
    str = str.slice(0, -1);
  }
  const lastAsteriskIndex = str.lastIndexOf('*');
  return str.substring(lastAsteriskIndex + 1);
}







var dialedCode = this.session.inputs['undefined'];
if (dialedCode.endsWith('#')) {
    dialedCode = dialedCode.slice(0, -1);
 }
const lastAsteriskIndex = dialedCode.lastIndexOf('*');
var payCode =  str.substring(lastAsteriskIndex + 1);

this.logger.info("The dialedCode is >>",dialedCode);
this.logger.info("The payCode is >>",payCode); 
this.session.setSessionValue('payCode',payCode);


var apiResponse = this.actionResults['getTransactionDetails'];
var responseStatus = apiResponse.status;
if(responseStatus=='00'){
   this.session.setSessionValue('payAmount',apiResponse.amount);
   this.session.setSessionValue('payDescription',apiResponse.description);
}else{
    this.goto("invalidPaymentCode")
}






function extractLastSegment(str) {
  if (str.endsWith('#')) {
    str = str.slice(0, -1);
  }
  const lastAsteriskIndex = str.lastIndexOf('*');
  return str.substring(lastAsteriskIndex + 1);
};
this.logger.info("The session val",this.session);
// var dialedCode = this.session.inputs['undefined'];
// var payCode = extractLastSegment(dialedCode);
// this.logger.info("The dialedCode is >>",dialedCode);
// this.logger.info("The payCode is >>",payCode); 
// this.session.setSessionValue('payCode',payCode);
this.session.setSessionValue('payCode',"617963");



var apiResponse = this.actionResults['signupApiCall'];
 this.logger.info("Rest Api Response>>",apiResponse);
var responseStatus = apiResponse.status;
if(responseStatus =='SUCCESS'){
   
    this.session.setSessionValue('accessToken',apiResponse.data);
}else{
    this.goto("signupFailed");
}

 


var apiResponse = this.actionResults['verifyAhcTicket'];
var responseStatus = apiResponse?.status || "FAILED";
if(responseStatus==="SUCCESS"){
     this.goto("ticketVerifySuccess")
}else{
    this.goto("ticketVerifyFailed");
}
```