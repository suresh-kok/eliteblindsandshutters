<!doctype html>
<html class="no-js" lang="en">
  <head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Elite Blinds &amp; Shutters</title>
    <link rel="stylesheet" href="css/foundation.css" />
    <link rel="stylesheet" href="css/style.css" />
    <script src="js/vendor/modernizr.js"></script>
    <link rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/highlight.js/8.0/styles/monokai.min.css">
    <link href="css/foundation-icons.css" rel="stylesheet">
    <script src="js/ga.js" async type="text/javascript"></script>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <meta name="SKYPE_TOOLBAR" content ="SKYPE_TOOLBAR_PARSER_COMPATIBLE"/>
<meta name="format-detection" content="telephone=no" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0">
<link href='http://fonts.googleapis.com/css?family=Lato:400,100,300,700,900' rel='stylesheet' type='text/css' />
<link rel="stylesheet" href="backslider/css/supersized.css" type="text/css" media="screen" />
<link rel="stylesheet" href="backslider/theme/supersized.shutter.css" type="text/css" media="screen" />
	<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js"></script>
		<script type="text/javascript" src="backslider/js/supersized.3.2.7.min.js"></script>
		<script type="text/javascript" src="backslider/theme/supersized.shutter.min.js"></script>	
     <script type="text/javascript">
			jQuery(function($){
				$.supersized({
					// Functionality
					slide_interval          :   3000,		// Length between transitions
					transition              :   5, 			// 0-None, 1-Fade, 2-Slide Top, 3-Slide Right, 4-Slide Bottom, 5-Slide Left, 6-Carousel Right, 7-Carousel Left
					transition_speed		:	700,		// Speed of transition
					// Components							
					slide_links				:	'blank',	// Individual links for each slide (Options: false, 'num', 'name', 'blank')
					slides 					:  	[			// Slideshow Images
														{image : 'images/01.jpg', title : 'Elite Blinds &amp; Shutters'},
														{image : 'images/02.jpg', title : 'Elite Blinds &amp; Shutters'},
														{image : 'images/03.jpg', title : 'Elite Blinds &amp; Shutters'},
												]
				});
		    });
		</script>   
  </head>
  <body>
<a id="prevslide" class="load-item"></a>
<a id="nextslide" class="load-item"></a>
  
  

  <div class="row">
  <div class="large-12 medium-12 columns">
  <div class="large-3 medium-3 columns">
  <div class="logo"> 
  <a href="index.html" title="Elite Blinds &amp; Shutters"><img src="images/logo.jpg" alt="Elite Blinds &amp; Shutters"></a>
  </div>
  <div class="sticky ">
  <nav role="navigation" data-topbar="" class="top-bar">
  <ul class="title-area">
    <!-- Title Area -->
    <li class="name">

    </li>
    <!-- Remove the class "menu-icon" to get rid of menu icon. Take out "Menu" to just have icon alone -->
    <li class="toggle-topbar menu-icon"><a href=""><span>Menu</span></a></li>
  </ul>

  
<section class="top-bar-section" style="left: 0px;">
    <ul class="text-center">
       <li><a href="index.html" title="Home">HOME</a></li>
       <li><a href="about.html" title="About Us">ABOUT US</a></li>
       <li class="has-dropdown active"><a href="collections.html" title="COLLECTIONS">COLLECTIONS</a>
       <ul class="dropdown">
       		<!--<li><a href="collections.html" title="Collections">COLLECTIONS</a></li>-->
       		<li class="has-dropdown"><a href="#" title="Plantation Shutters">PLANTATION SHUTTERS</a>
            	<ul class="dropdown">
                	<!--<li><a href="plantation-shutters.html" title="Plantation Shutters">PLANTATION SHUTTERS</a></li>-->
                	<li><a href="basswood-shutters.html" title="Basswood Shutters">BASSWOOD SHUTTERS</a></li>
                    <li><a href="cedar-shutters.html" title="Cedar Shutters">CEDAR SHUTTERS</a></li>
                    <li><a href="pvc-shutters.html" title="PVC Shutters">PVC SHUTTERS</a></li>
                    <li><a href="aluminium-shutters.html" title="Aluminium Shutters">ALUMINIUM SHUTTERS</a></li>
                </ul>
            </li>
            <li><a href="venetian-blinds.php" title="Venetian Blinds">VENETIAN BLINDS</a></li>
            <li><a href="roller-blinds.php" title="Roller Blinds">ROLLER BLINDS</a></li>
            <li><a href="vertical-blinds.php" title="Vertical Blinds">VERTICAL BLINDS</a></li>
            <li><a href="panel-glides.html" title="Panel Glides">PANEL GLIDES</a>
            <li><a href="flyscreens.html" title="Flyscreens">FLYSCREENS</a>
            <li><a href="indoor-outdoor.html" title="Indoor &amp; Outdoor Furniture">INDOOR &amp; OUTDOOR FURNITURE</a></li>
             
            <li><a href="security-doors.html" title="Security Doors">SECURITY DOORS</a></li>
            <li><a href="flooring.html" title="Flooring">FLOORING</a></li>
            <li><a href="painting-wallart.html" title="Paintings &amp; Wall Art">PAINTINGS &amp; WALL ART</a></li>
            <li><a href="homewares.html" title="Homewares">HOMEWARES</a></li>
       </ul>
       </li>
       <li><a href="promotions.html" title="Promotions">PROMOTIONS</a></li><li><a href="contact-us.html" title="Contact">CONTACT</a></li>
    </ul>
    <!-- Right Nav Section -->
    
  </section></nav>
  </div>
  </div>
  <div class="large-9 medium-9 columns">
  <div class="large-12 medium-12">
<div class="call-sec">
	<a href="tel:1300 892 674">1300 892 674</a>
</div>
</div>
<div class="clearfix"></div>
<div class="large-11 medium-11 columns pad-left">
<div class="wrapper">
<h1>Confirm Your Order</h1>
<?php
error_reporting(0);
session_start();
?>

 <script>
 function validate_form ( )
{
    valid = true;
	var validRegExp = /^[^@]+@[^@]+.[a-z]{2,}$/i;
	var strEmail = document.contact.contact_email.value;

    if ( document.contact.contact_name.value == "" )
    {
        alert ( "Please fill in the Name Feild." );
        valid = false;
    }
	
	else if (strEmail.search(validRegExp) == -1) 
	{
		alert ( "Please enter a valid email address. " );
		valid = false;
	} 
 	
	else if ( document.contact.address.value == "" )
    {
        alert ( "Please fill in Your Address" );
        valid = false;
    }
		else if ( document.contact.suburb.value == "" )
    {
        alert ( "Please fill in Your Suburb" );
        valid = false;
    }
 

    return valid;
}
</script>
<script type="text/javascript">
/* <![CDATA[ */
var google_conversion_id = 1011673845;
var google_conversion_language = "en";
var google_conversion_format = "2";
var google_conversion_color = "ffffff";
var google_conversion_label = "bo9PCMvf4QEQ9dWz4gM";
var google_conversion_value = 0;
/* ]]> */
</script>
<script type="text/javascript"
src="http://www.googleadservices.com/pagead/conversion.js">
</script>
<noscript>
<div style="display:inline;">
<img height="1" width="1" style="border-style:none;" alt=""
src="http://www.googleadservices.com/pagead/conversion/1011673845/?label=bo9P
CMvf4QEQ9dWz4gM&amp;guid=ON&amp;script=0"/>
</div>
</noscript>

<form action="QuoteSent.php?sent=yes" name="contact" method="post" id="contact_form"   onsubmit="return validate_form ( );">       
  <div class="large-12 medium-12 small-12">
    <div class="large-6 medium-12 small-12 columns">      
      <?php
	$counter=0;
	$total = 0;
	$delivery = number_format($_POST['delivery'],2);
	foreach($_POST['item'] as $order=>$item){
	//while($order <= $order_num){
		$total = $total+number_format($item['price'],2);
		$image = explode(" ",$item['material']);
		$image = strtolower(str_replace("Basswood","bw",$image[0]));
		$image = str_replace("sun","sunscreen",$image);
		$image .= "_".strtolower(str_replace(" ","_",str_replace("Light/Medium","light",str_replace("Medium/Dark","dark",$item['colour'])))).".jpg";
      ?>
      <h3 style="float:left; clear:left;">Blind <?=($counter+1)?></h3><br/><br/>
      <div class="">
	<img src="images/order/<?=$image?>" alt="<?=$item['material']?> <?=$item['colour']?>" style="float:left;display:none;"/>
	<table  cellspacing="0" cellpadding="3" class="order_info">
	  <tr>
		  <td style="width:20%;"><strong>Material:</strong></td>
		  <td style="text-align:left; width:80%;"><?=$item['material']?></td>
	  </tr>
	  <tr>
		  <td><strong>Colour:</strong></td>
		  <td style="text-align:left;"><?=$item['colour']?></td>
	  </tr>
	  <tr>
		  <td><strong>Qty:</strong></td>
		  <td style="text-align:left;"><?=$item['qty']?></td>
	  </tr>
	  <tr>
		  <td><strong>Width:</strong></td>
		  <td style="text-align:left;"><?=$item['width']?>mm</td>
	  </tr>
	  <tr>
		  <td><strong>Height:</strong></td>
		  <td style="text-align:left;"><?=$item['height']?>mm</td>
	  </tr>
	  <tr>
		  <td><strong>Fitted:</strong></td>
		  <td style="text-align:left;"><?=$item['fitted']?></td>
	  </tr>
	  <tr>
		  <td><strong>Price:</strong></td>
		  <td style="text-align:left;">$<?=number_format($item['price'],2)?></td>
	  </tr>
	</table>
      </div>
      <input type="hidden" name="item[<?=$counter?>][qty]" value="<?=$item['qty']?>">
      <input type="hidden" name="item[<?=$counter?>][material]" value="<?=$item['material']?>"> 
      <input type="hidden" name="item[<?=$counter?>][colour]" value="<?=$item['colour']?>">  
      <input type="hidden" name="item[<?=$counter?>][fitted]" value="<?=$item['fitted']?>"> 
      <input type="hidden" name="item[<?=$counter?>][width]" value="<?=$item['width']?>">
      <input type="hidden" name="item[<?=$counter?>][height]" value="<?=$item['height']?>">
      <input type="hidden" name="item[<?=$counter?>][price]" value="<?=$item['price']?>">
      <?
	      $counter++;
      }
      ?>
      <input type="hidden" name="delivery" value="<?php print($delivery);?> ">
	      <input type="hidden" name="readymade" value="<?php print($_POST['readymade']); ?>">
	      <input type="hidden" name="total" value="<?php print($total+$delivery); ?> ">
      <input type="hidden" name="deposit" value="<?php 
		    $depo= $_POST['total']/2;
		    $depo=round($depo,2);
	     print($depo);    ?>">
    </div><!--large6-->
    <div class="large-6 medium-12 small-12 columns">
      <div class='form_heading'>Fill Out the form to Send us your order</div>
      <div class="large-12 small-12"> Your Name:</div>
      <div class="large-12 small-12"><input type="text" name="contact_name" value="<?php if($feedback=="2") { print($contact_name); }?>"/></div>
      <div class="large-12 small-12">Your Email:</div>
      <div class="large-12 small-12"><input type="text" name="contact_email" value="<?php if($feedback=="2") { print($contact_email); }?>"/></div>
      <div class="large-12 small-12">Your Mobile Phone:</div>
      <div class="large-12 small-12"><input type="text" name="contact_mobile" value="<?php if($feedback=="2") { print($contact_mobile); }?>"/></div>
      <div class="large-12 small-12">Contact Phone:</label></div>
      <div class="large-12 small-12"><input type="text" name="contact_phone" value="<?php if($feedback=="2") { print($contact_phone); }?>"/></div>
      <div class="large-12 small-12">Delivery Address :</label></div>
      <div class="large-12 small-12"><input type="text" name="address" value="<?php if($feedback=="2") { print($address); }?>"/></div>
      <div class="large-12 small-12">Suburb:</label></div>
      <div class="large-12 small-12"><input type="text" name="suburb" value="<?php if($feedback=="2") { print($suburb); }?>"/></div>
      <div class="large-12 small-12">State/Postcode:</label></td>
      <div class="large-12 small-12">
	    <select name="state">
		    <option>VIC</option>
		    <option>NSW</option>
		    <option>QLD</option>
		    <option>ACT</option>
		    <option>SA</option>
		    <option>WA</option>
		    <option>TAS</option>
		    <option>NT</option>
	    </select>
      </div>
      <div class="large-12 small-12">Postcode:</label></div>
      <div class="large-12 small-12"><input type="text" name="postcode" value="<?php if($feedback=="2") { print($postcode); }?>"/></div>
      <div class="large-12 small-12">Further Comments Or Questions</td>
      <div class="large-12 small-12"><textarea name="query_description"> <?php if ($feedback=="2") {print($query_description);} ?></textarea></div>
      <div class="large-12 small-12"><input name="contact" type="submit" value="Send Your Order" class="button"/></div>
      </div>  
  </div><!--large6-->
  </div><!--large12-->
  
  <div class="small-12 large-12 medium-12 columns">
    <div class="clearfix"></div>
    <br/>
    <strong>Notes:</strong>
    <ul class="ul-notes">
	<li>(We Do Not Deliver To PO Boxes)</li>
	<li>
	    <?php
	    if ($_POST['readymade'] == "Ready") {
		print('Upon receiving your order you will receive an email to confirm your order and payment. Should you not have received such email within 24 hours please contact us.');
	    } else {
		print('Upon receiving your order one of our sales staff will contact you to confirm your order and to arrange your deposit payment');
	    }
	    ?> 
	</li>
    </ul>
  </div>
</form>
  
<div class="box half" style="clear:left;">
<strong style="float:left; margin-left:20px;">Delivery: $<?=$delivery?></strong>
<div class="order_price" style="clear:left;">Total: <span class="price">$<?=number_format(($total+$delivery),2)?> (inc delivery)</span></div>
</div>





















<div class="clearfix"></div>
<h2>YOUR BUSINESS IS MORE IMPORTANT TO US THAN A QUICK SALE!</h2>


<div class="clearfix"></div>
</div>
</div>
<div class="large-1 medium-1 columns">&nbsp;</div>
  </div>
  </div>
  </div>
 
 
 <div class="clearfix"></div> 
  <div class="row"> 
   <div class="large-12 medium-12 columns"> 
  <div class="large-6 medium-6 columns"> 
<div class="foot-inner">

<div class="foot-text1">  
  ELITE BLINDS &amp; SHUTTERS &copy; 2015 &nbsp; | &nbsp; <a href="privacy.html" title="Privacy Policy">PRIVACY POLICY</a> 
</div>
</div>
</div>

<div class="large-6 medium-6 columns">&nbsp;</div>
</div>
</div>
</div>

  
  
  
   
    <script src="js/vendor/jquery.js"></script>
    <script src="js/foundation.min.js"></script>
  
       <script src="js/all.js"></script>
    
    <script>
      $(document).foundation().foundation('joyride', 'start');
    </script>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
		<script type="text/javascript" src="js/scripts.js"></script>
        
        


<script>
  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','//www.google-analytics.com/analytics.js','ga');

  ga('create', 'UA-60850150-1', 'auto');
  ga('send', 'pageview');

</script>
	</body>
  
</html>
