   M S B A M L   `   `   `  ����  Camping_UI_IndexM8clr-namespace:Camping_UI_Index;assembly=Camping_UI_IndexCamping_UI_Index  5   	    
ViewModels;,clr-namespace:ViewModels;assembly=ViewModels
ViewModels 5   	   ? ;Menu, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null) clr-namespace:Menu;assembly=MenuMenu 5   	   ;,clr-namespace:ViewModels;assembly=ViewModels
ViewModels 67   !    Camping_UI_Index.MainWindow   5   	   Blocal8clr-namespace:Camping_UI_Index;assembly=Camping_UI_Index  5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;vm,clr-namespace:ViewModels;assembly=ViewModels     5   	   1Menu clr-namespace:Menu;assembly=Menu     5   	   <vm1,clr-namespace:ViewModels;assembly=ViewModels     67     � Title$  Joehoe Campings��5
   	   $	��600��5   	   $	��800��5   	    � AllowsTransparency$
 True��5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$Y Spack://application:,,,/CampingApp2;component/Resources/Images/Icons/icoKangaroo.ico��5   	    � 
ResizeMode$ CanResizeWithGrip=�5   	   $	��800��5   	   $	��600��5   	    � 	Resources 5         HoverButtonStyle �� 
TargetType �� Property	 �� Value
 �� 
TargetType �� Triggers P� Property P� Value��%  &	        �� 5   
   " ��6)   .�5      ��  � ��	 �	#FF282C316+   �� 5       � ��	 �	#FF2C9BFF6+   �� 5       � ��$	 0q�60   �� 5       �@�FocusVisualStyle	 61   Z� �� 5       � ��	 5      �� 5      "
 ��6&   .R�5      �� #��z��6!   #��z��5    !   #��z��5!   !   ._�5"      �� $��Center=�6/   $��Center=�6L   5#       5$      P� 5%       �@J�IsMouseOver$
 True��6=   . �5&   "   ��  � ��	 �	#FF3A3E446?   5'      5)      5+      5,      5-      .��5/      �� ���	#FF0E3A64$��2q�6%   ._�52   
   � -   5        $��gridSideBar��52      ���#20262B.+�54      � -   5         �Name$ 	pnlTopBar��54      ���#282C31$��50��6;   $	��Top=�6G   .+�56      ��  �� Orientation$ 
Horizontal=�6   58      �� 5:      -   5         ��Name$ lblLoggedIn��5:      $�� ��6-   $
��Left=�68   $��
181,10,0,0q�6S   $	��Top=�6g   $��40��6   $	��459��6�   $��14(�6�   $��	Yu Gothic)�6�   
�������6�   �� 5=      $ 
Horizontal=�6   $��Right=�66   .+�5?      �� -   5         ��Name$ btnMinimize��5?      $	��➖��62   #��[  6>   $��50��6h   $��50��6s   $��Century Gothic)�6   6�   �� 5@      -   5        $ btnFullscreen��5@      $
��🔲��64   #��[  6A   $��50��6k   $��50��6v   $��Century Gothic)�6�   6�   �� 5A      -   5        $ btnTerminate��5A      $��✖️��63   #��[  6@   $��1,0,0,0q�6j   $��50��6{   $��50��6�   $��Century Gothic)�6�   6�   5B      5C      �� 5F      -   5         ��Name$ pnlSelectionBar��5F      ���#282C31$	��180��6G   $ Vertical=�6S   $
��Left=�6j   .+�5G      �� $	��100��6   $	��100��6%   $Z��Tpack://application:,,,/CampingApp2;component/Resources/Images/imgRoundedKangaroo.png��61   $��0,20,0,0q�6�    J� Effect 5H      3  -System.Windows.Media.Effects.DropShadowEffect  5I         ShadowDepth$ 0Q�6+      Color$ 	#FF2B88CA��6;      Opacity$ 1Q�6M      
BlurRadius$ 65Q�6Y   5K      5L      o� 5O      
������ $	��180��5P      $
��Left=�5Q      $��	0,155,0,0q�5R      .@�5T        Menu.Btn    BtnStyle#��[ 6    d� 	IsChecked$
 TrueY�5V      $��-5,0,0,0q�5W      ��5U      �� *HomeCommand+.��5Y      � .+�5Z      �� $
��Home��6$     
Text_Style#��[ 5[   $   5\      5^        5`      #��[ 6   $��-5,0,0,0q�5b      ��5a      �� *ReservationCommand+.��5d      � .+�5e      �� $��Reservation��6$   #��[ 5f   $   $��0,16,0,0q�6H   $��92��6Z   $	��Top=�6e   $��Center=�6}   5g      5h        5j      -   5          Name$ loginBtn��5j      #��[ 6-   $��-5,0,0,0q�5l      ��5k      �� *LoginCommand+.��5n      � .+�5o      �� $��Login��6$   #��[ 5p      $��Stretch=�6=   $��	67,0,67,0q�6[   5q      5r        5s      -	   5        $ accInfo��5s      #��[ 6,   $	��178��5t   <   $
��Left=�6H   $��-5,0,0,0q�6c   ��6   �� *AccountCommand+.��5v      � .+�5w      �� $��
My Account��6$   #��[ 5x      $��40,0,0,0q�68   $	��113��6J   5y      5{        5|      -
   5        $ btnAdminPage��5|      #��[ 62   $	��178��5}   :   $
��Left=�6F   $��-5,0,0,0q�6a   ��6   �� *AdminCommand+.��5~      � .+�5      �� $��Admin��6$   #��[ 5�      $��	63,0,64,0q�6A   $��Stretch=�6T   5�      5�      5�      � 5�      -   5        $ pnlMain��5�      ���#20262B$��Stretch=�69   $��Stretch=�6W   $��
180,50,0,0q�6s    � RowDefinitions 5�      �� 5�      $��* �6$    � ColumnDefinitions 5�      �� 5�      $��* �6'   .+�5�      �� -   5         ��Name$
 Home��5�   !   $��0��6/   $��0��6<   ��6L   �� *CurrentView+5�      5�      5�      5�      