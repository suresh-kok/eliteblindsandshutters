<?php 

$bcc = "enquiries@directclicks.com.au";
$to = "tahirm@eliteblindsandshutters.com.au";
//$to = "tahirm@eliteblindsandshutters.com.au,enquiries@directclicks.com.au";
//$to = "testing007web@gmail.com";
$error = "";
$phoneno = $_POST['phone'];

if(($_POST['phone']=="") || $_POST['phone']=="Phone*")
{
	$error .= "Please enter Phone.<br />";	
}
if(!is_numeric($phoneno))
{
	$error .= "Please enter phone in numeric.<br />";
}
if(strlen($phoneno)<8)
{
	$error .= "Phone no. should be minimum 8 characters.<br />";
}

if(($_POST['email']=="") || $_POST['email']=="Email")
{
	$error .= "Please enter valid email address.<br />";	
}


if($error=="")
{
	$from = $_POST["email"];
	$subject = "800242 - Enquiry From Your Website";
	$message = '
	<html>
	<body>';
	if($_POST["name"]!=""){
		$message .= '	<strong>Name:</strong>  '.$_POST["name"].'<br/><br/>';
	}
	
	$message .= '	<strong>Phone:</strong>  '.$_POST["phone"].'<br/><br/>
					<strong>Email:</strong> '.$_POST["email"].'<br/><br/>';
	if($_POST["location"]!=""){						
		$message .= '	<strong>Location:</strong> '.$_POST["location"].'<br/><br/>';
	}
	
	if($_POST["wallartid"]!=""){						
		$message .= '	<strong>Wallartid:</strong> '.$_POST["wallartid"].'<br/><br/>';
	}
	
	if($_POST["state"]!=""){						
		$message .= '	<strong>State:</strong> '.$_POST["state"].'<br/><br/>';
	}
					
	if($_POST["message"]!=""){						
		$message .= '	<strong>Comments:</strong> '.$_POST["message"].'<br/><br/>';
	}	
	$message .= '</body></html>';
	
	$headers  = "From: $from\r\n";
	$headers  .= "BCC: $bcc\r\n";
	$headers .= "Content-type: text/html\r\n";
	$mail = mail($to, $subject, $message, $headers);
	
	//echo $message;
	header('Location: thanks.html');
	exit();
	
}else{ 
	echo $error;
}

?>

