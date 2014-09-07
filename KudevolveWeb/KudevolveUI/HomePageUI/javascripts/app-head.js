// Toggle	

jQuery(window).load(function(){
     
    $('.toggle-view li').click(function () {
 
        var text = $(this).children('div.toggle-content');
 
        if (text.is(':hidden')) {
            text.slideDown('200');
            $(this).children('span').html('<i class="icon-minus"></i>');    
        } else {
            text.slideUp('200');
            $(this).children('span').html('<i class="icon-plus"></i>');    
        }
         
    });
 
});

// Navigation slide down

 $(function(){
    $('.dropdown').hide();
    $('li.has-dropdown').hover(
        function () {
            var target = $(this).children('ul.dropdown');
            $.browser.msie ? target.show() : target.slideDown(400);
        },
        function () {
            var target = $(this).children('ul.dropdown')
            $.browser.msie ? target.hide() : target.slideUp(400);
        }
    );
});

// Carousel

$(window).load(function(){


			//	Responsive layout, resizing the items
			$('#carousel-works').carouFredSel({
				responsive: true,
				width: '100%',
				auto: false,
				circular	: false,
				infinite	: false,
				prev : {
					button		: "#car_prev",
					key			: "left",
						},
				next : {
					button		: "#car_next",
					key			: "right",
							},
				swipe: {
					onMouse: true,
					onTouch: true
					},
				items: {
					visible: {
						min: 1,
						max: 4
					}
				}
			});
			
			//	Responsive layout, resizing the items
			$('.carousel-type2').carouFredSel({
				responsive: true,
				width: '100%',
				auto: false,
				circular	: false,
				infinite	: false,
				prev : {
					button		: "#car_prev2",
					key			: "left",
						},
				next : {
					button		: "#car_next2",
					key			: "right",
							},
				swipe: {
					onMouse: true,
					onTouch: true
					},
				items: {
					visible: {
						min: 1,
						max: 1
					}
				}
			});

		});

// Tooltips Tipsy 

jQuery(window).load(function() {
			$('.has-tipsy').tipsy({gravity: $.fn.tipsy.autoNS, fade:true});
});



// Accordion

$(document).ready(function() {
	var cur_stus;
	
	//close all on default
	$('.accordion .accordion-content').hide();
	$('.accordion .accordion-title').attr('stus', '');
	       
	//open default data
	$('.accordion .accordion-content:eq(0)').slideDown();
	$('.accordion .accordion-title:eq(0)').attr('stus', 'active').addClass('active');

	$('.accordion .accordion-title').click(function(){
		cur_stus = $(this).attr('stus');
		if(cur_stus != "active")
		{
			//reset everthing - content and attribute
			$('.accordion .accordion-content').slideUp();
			$('.accordion .accordion-title').attr('stus', '').removeClass('active');
			
			//then open the clicked data
			$(this).next().slideDown();
			$(this).attr('stus', 'active').addClass('active');
		}
		//Remove else part if do not want to close the current opened data
		else
		{
			$(this).next().slideUp();
			$(this).attr('stus', '').removeClass('active');
		}
		return false;
	});
});


// Titan Light Box 

jQuery(document).ready(function($) {
		$('.titan-lb').lightbox({
			'scrolling': 'auto',
			theme: 'default'
		});
		 prettyPrint();
	});

// BACK TO TOP
  
$(document).ready(function(){
 
        $(window).scroll(function(){
            if ($(this).scrollTop() > 100) {
                $('.scrollup').fadeIn();
            } else {
                $('.scrollup').fadeOut();
            }
        });
 
        $('.scrollup').click(function(){
            $("html, body").animate({ scrollTop: 0 }, 600);
            return false;
        });
 
});


// Overlay

    $(document).ready(function(){
    $(".image-overlay .overlay-icon").fadeTo("fast", 0); // This sets the opacity of the thumbs to fade down to 60% when the page loads
     
    $(".image-overlay .overlay-icon").hover(function(){
    $(this).fadeTo("fast", 0.6); // This should set the opacity to 100% on hover
    },function(){
    $(this).fadeTo("fast", 0); // This should set the opacity back to 60% on mouseout
    });
    });