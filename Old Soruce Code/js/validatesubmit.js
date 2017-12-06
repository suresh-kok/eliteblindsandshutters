function clickclear(thisfield, defaulttext) {
	if (thisfield.value == defaulttext) {
		thisfield.value ="";
	}
}
function clickrecall(thisfield, defaulttext) {
	if (thisfield.value == "") {
		thisfield.value = defaulttext;
	}
}

function isValidEmail(str) {

   return (str.indexOf("@") > 0);

}

function IsNumeric(sText)
{
   var ValidChars = "0123456789.";
   var IsNumber=true;
   var Char;

 
   for (i = 0; i < sText.length && IsNumber == true; i++) 
      { 
      Char = sText.charAt(i); 
      if (ValidChars.indexOf(Char) == -1) 
         {
         IsNumber = false;
         }
      }
   return IsNumber;
   
   }
   
function alltrim(str) 
{
                return str.replace(/^\s+|\s+$/g, '');
 }



function validContactForm()
{

if(alltrim(document.form1.phone.value) == "Phone*"|| alltrim(document.form1.phone.value) == "" )
	{
		alert("Please enter your phone number");
		document.form1.phone.focus();
		return false;
	}

if (!IsNumeric(document.form1.phone.value))
{
alert ("Phone no. must be numeric");
document.form1.phone.focus();
return false;
}

if ((document.form1.phone.value.length < 8) || (document.form1.phone.value.length > 10)) {
alert ("Phone no. should be min 8 & max 10 characters.");
document.form1.phone.focus();
return false;
}



if (document.form1.email.value=="Email*"||document.form1.email.value=="")
{
alert ("Please enter your email.")
document.form1.email.focus();
return false;
}


emailExp= /^\w+(\-\w+)*(\.\w+(\-\w+)*)*@\w+(\-\w+)*(\.\w+(\-\w+)*)+$/;
	 	 if (!(emailExp.test(document.form1.email.value)))
		{ 
			   alert("Invalid email address");
			   document.form1.email.focus();
			   return false;  
		} 
 		
}

