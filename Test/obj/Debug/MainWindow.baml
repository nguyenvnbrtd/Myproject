   M S B A M L   `   `   `  ����?  ;Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TestTest  5   	       Test.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Test     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   -   5        $��
mainwindow��5
   	    
  MainVM  � DataContext#  [  5   	    � Title$ 
MainWindow��5   	   $	��440��6   $	��770��6)    � 	Resources 5        imageButton �� 
TargetType �� Triggers :� RoutedEvent g� Duration �� TargetProperty	 W� To	 �� Property
 �� Value��%�  &	       �� 5   
   " ��6$    5      :� 5      $ 
MouseEnter��6   [�5      �� 5      .��5      �� .��5   "   W� $ 	0:0:0.100D�63   $ Width�6H   $	 520X�6j   6u   5      5      5      :� 5      $ 
MouseLeave��6   [�5      �� 5      .��5      �� .��5   "   W� $ 	0:0:0.100D�62   $ Width�6G   $	 500X�6i   6t   5      5       5"      :� 5#      $ 	MouseDown��6   [�5$      �� 5%      .��5&      �� .��5'   "   W� $ 0:0:0D�62   $ Width�6C   $	 500X�6e   6p   W� 5(   "   $ 0:0:0.1D�62   $ Width�6E   $	 520X�6g   6r   5*      5+      5-      .�50      �� 	 � ��$	
 500��6&   64   51      53      .  (System.Windows.Interactivity.Interaction   Triggers 55      /  )System.Windows.Interactivity.EventTrigger  56   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 57      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedWindowCommand+   CommandParameter 6L   ��  �� ElementName$ 
mainwindow��58      .��5;      � 
�������.+�5=   
   �� $��illustration/test2.jpg��6   $��	4,8,-4,-8q�61   5?      �� 5@   
   -   5        $��imageC��5B      $��illustration/test.png��5@      #��[ 5A       5F        5G      $ 	MouseDown��6!   . 5H         6,   �� *ClickCommand+5I      5K      � 5L   
   $	��200��6   $	��200��6   .+�5M      �� $	��200��5N      $	��100��6   
�������$��50(�5P   (    �� TextAlignment$ Center=�5Q      $��229,-54,-229,154q�5R      ��5P      �� *Timer+5R   -   �� 5S      $	��150��6   $��50��6!   $��25(�6-   $��254,75,-204,75q�6;   .��6S   Reset 5T        5U      $ Click��6%   . 5V         60   �� *ResetCommand+5W      5Y      5[      5_      5`      