   M S B A M L   `   `   `  ����@  <Timer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TimerTimer  5   	       Timer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Timer     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          �Name$  MainWD��5   	      MaterialDesignBody#���   5	      $��Regular"�5
      $��13(�5      &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5       � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � Title$ 
MainWindow��5   	   $	��350��6   $	��300��6)   .��5      � $	��450��6   $	��300��6   .+�5   
   �� -   5        $��40��5      $��40��6   $	��Top=�6(   $��Center=�6@   ���	#FFF18484$��126,33,134,0q�5   T   ��6(   Z� 5      '  !MaterialDesignThemes.Wpf.PackIcon  5   
   -   5        $��40��5   "   $��40��6-   $	��Top=�5      $��Center=�5         Kind$ Close=�5      ���	#DD5B5B5B$��126,34,134,0q�5    A   5!   !   � 5#   
   $��80��6   $��80��6   $��63,219,157,151q�6'   .+�5$      �� -   5        $��75��5$      $��75��6    ��5%      
� 5&      .��5'      �  � Color$ 	#FFD1D1D1��6'    � Offset$ 0.305Q�69   � 5(      $ 	#FF6E97E0��6'   $ 1Q�69   5)      5+        5,      -   5        	  Name$	 PlayIcon��5-   "   $��75��5,   '   $��75��62   $
 Play=�6>   ���	#DD363A4D$��Stretch=�5.   9   $��Stretch=�5/   A     52      -   5        $	 	PauseIcon��53   "   $ Pause=�52   &   $��75��63   $��75��6>   $y�Hidden=�53   5   ���	#DD363A4D55   %   56      � 58   
   $��80��6   $��80��6   $��152,220,68,150q�6)   .+�59      �� -   5        $��75��59      $��75��6    ��6,   Z� ��5:      
� 5;      .��5<      � $ 	#FFE8AAAA��6'   $ 0.016Q�69   � 5=      $ 	#FFC56161��6'   $ 1Q�69   5>      5@        5A      -   5        $	 StopIcon��5B   "   $
 Stop=�5A   &   $��75��62   $��75��6=   ���	#DD363A4D$��Stretch=�5C   Z   $��Stretch=�6x   5D   %   5F      �� 5H   
   $��30(�6   $��50��67   
 �� TextAlignment$
 Right=�6C   $��70,128,157,272q�6Y   ��6"   �� *Sec+6Y   �� 5I   
   $��30(�6   $��50��6"   $��142,128,85,272q�6.   .6I   �� 5J   
   $��30(�6   $��50��67   $

 Left=�6C   $��151,128,76,272q�6X   ��6"   �� *Mil+6X   � 5K   
   -	   5         �Name$ TimerBar��5K       � Minimum$ 0Q�6(    � Maximum$ 10000Q�64   $��10��6D   $��20,178,30,262q�6P    J� RenderTransformOrigin$ 0.5,0.5&�6g   5M      5N      5O      