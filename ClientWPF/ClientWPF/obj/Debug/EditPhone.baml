   M S B A M L   `   `   `  ����D  @ClientWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClientWPF	ClientWPF  5   	       ClientWPF.EditPhone   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ClientWPF     5   	     � Title$  	EditPhone��5   	   $	��350��6   $	��350��6(    � 	Resources 5	      $   ClientWPF.Base64ImageConverter   Base64ImageConverter��%   &	          5
   
   6%   5      .��5      � ���	#FF3F2D20 � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $��* �6   .+�5   
   �� $��2��6   $-��'Редактировать модель��6(   $��Center=�6D   $��10q�6a   $��26(�6m   
�������6{   �� 5   
   $��1��6   $��Модель:��6!   $��Center=�60   $��10q�6M   $��14(�6Y   
�������6g   �� 5   
   -   5         ��Name$ tb_model��5      $��1��6F   $��1��6S   $��10q�6c   $��14(�6o   
���   ���6$   ��  �� Path$ phone.Model�6}   �� 5   
   $��2��6   $��	Фото:��6!   $��Center=�6.   $��10q�6K   $��14(�6W   
�������6e   �� 5   
   -   5        $��
btn_Insert��5   .   $��2��6   $��1��6   $��30��6A   $��10q�6M   $-��'Выберите изображение��6m   �� 5   
   -   5         ��Name$ imgPhonePhoto��5   -   $��3��6   $��0��6   $��10q�6�   ��6D   �� $ phone.base64Image� �� 	Converter# [  6�   �� 5    
   -   5        $��btn_Insertt��5    .   $��3��6   $��1��6   $��10q�6A   $��30��6M   $ ��Редактировать��6m   5!      5"      