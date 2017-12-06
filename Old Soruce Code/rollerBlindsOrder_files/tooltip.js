/*
 * Image preview script 
 * powered by jQuery (http://www.jquery.com)
 * 
 * written by Alen Grakalic (http://cssglobe.com)
 * 
 * for more info visit http://cssglobe.com/post/1695/easiest-tooltip-and-image-preview-using-jquery
 *
 */
 
this.imagePreview = function(){	
	/* CONFIG */
	var xOffset = 10;
	var yOffset = 30;
	var mouseX = 0;
	var mouseY = 0;
	$(this).mousemove( function(e) {
	   mouseX = e.pageX;
	   mouseY = e.pageY;
		 if(mouseY > ($(document).height()-420)){
			//alert(mouseY+" "+$(document).height());
			xOffset = 430;
			yOffset = 30; 
		 }else{
			xOffset = 10;
			yOffset = 30; 
		 }
	 });
	 
	 
		
		
		// these 2 variable determine popup's distance from the cursor
		// you might want to adjust to get the right result
		
	/* END CONFIG */
	$("a.preview").hover(function(e){
		this.t = this.title;
		//this.title = "";	
		var c = (this.t != "") ? "" + this.t : "";
		$("body").append("<p id='preview'><span class='page_heading'><span>"+ c +"</span></span><br/><br/><img src='"+ this.href +"' alt='Image preview' /></p>");								 
		$("#preview")
			.css("top",(e.pageY - xOffset) + "px")
			.css("left",(e.pageX + yOffset) + "px")
			.fadeIn("fast");						
    },
	function(){
		this.title = this.t;	
		$("#preview").remove();
    });	
	$("a.preview").mousemove(function(e){
		$("#preview")
			.css("top",(e.pageY - xOffset) + "px")
			.css("left",(e.pageX + yOffset) + "px");
	});			
};


// starting the script on page load
$(document).ready(function(){
	imagePreview();
});