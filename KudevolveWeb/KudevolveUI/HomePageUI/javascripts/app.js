;(function ($, window, undefined) {
  'use strict';

  var $doc = $(document),
      Modernizr = window.Modernizr;

  
  $.fn.foundationAlerts           ? $doc.foundationAlerts() : null;
  $.fn.foundationAccordion        ? $doc.foundationAccordion() : null;
  $.fn.foundationTooltips         ? $doc.foundationTooltips() : null;
  $('input, textarea').placeholder();
  
  
  $.fn.foundationButtons          ? $doc.foundationButtons() : null;
  
  
  $.fn.foundationNavigation       ? $doc.foundationNavigation() : null;
  
  
  $.fn.foundationTopBar           ? $doc.foundationTopBar() : null;
  
  $.fn.foundationCustomForms      ? $doc.foundationCustomForms() : null;
  $.fn.foundationMediaQueryViewer ? $doc.foundationMediaQueryViewer() : null;
  
    
    $.fn.foundationTabs             ? $doc.foundationTabs() : null;
    
  
  
    $("#featured").orbit();
  

  // UNCOMMENT THE LINE YOU WANT BELOW IF YOU WANT IE8 SUPPORT AND ARE USING .block-grids
  // $('.block-grid.two-up>li:nth-child(2n+1)').css({clear: 'both'});
  // $('.block-grid.three-up>li:nth-child(3n+1)').css({clear: 'both'});
  // $('.block-grid.four-up>li:nth-child(4n+1)').css({clear: 'both'});
  // $('.block-grid.five-up>li:nth-child(5n+1)').css({clear: 'both'});

  // Hide address bar on mobile devices
  if (Modernizr.touch) {
    $(window).load(function () {
      setTimeout(function () {
        window.scrollTo(0, 1);
      }, 0);
    });
  }

})(jQuery, this);


// Slider Revolution

jQuery('.mainslider').revolution(
{
		delay:9000,
		startheight:490,
		startwidth:950,

		thumbWidth:100,
		thumbHeight:50,
		thumbAmount:4,

		onHoverStop:"on",
		hideThumbs:200,
		navigationType:"thumb",
		navigationStyle:"round",
		navigationArrows:"verticalcentered",

		touchenabled:"on",

		navOffsetHorizontal:0,
		navOffsetVertical:0,
		shadow:1,
		fullWidth:"off"
});


$(function() {
            $(".contentHover").hover(
                function() {
                    $(this).children(".content").fadeTo(200, 0.25).end().children(".hover-content").fadeTo(200, 1).show();
                },
                function() {
                    $(this).children(".content").fadeTo(200, 1).end().children(".hover-content").fadeTo(200, 0).hide();
                });
        });
		
// Flexi Slider



  <!-- Target sliders individually with different properties -->
  $(window).load(function() {
	  
    $('.simple-slider').flexslider({
        animation: "slide",
		slideshow: false,
		controlNav: false,
		smoothHeight: true,
        start: function(slider){
          $('body').removeClass('loading');
        }
      });
	
	$('.gallery-slider').flexslider({
        animation: "slide",
		controlNav: "thumbnails",
        start: function(slider){
          $('body').removeClass('loading');
        }
	});
 
    $('#main-slider').flexslider({
        animation: "slide",
		controlNav: false,
        start: function(slider){
          $('body').removeClass('loading');
        }
	});
  });


	
