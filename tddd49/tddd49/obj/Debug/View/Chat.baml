   M S B A M L   `   `   `  ����A  =tddd49, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:tddd49.Modeltddd49.Model  5      )clr-namespace:tddd49.Viewtddd49.View  5          tddd49.View.Chat   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (mclr-namespace:tddd49.Model     5      +localclr-namespace:tddd49.View     5      .��5
      � ���	#FFE5E5E5  �� Left$	  -15��6"   $	��639��64   $	��426��6@    � ColumnDefinitions 5   
   �� 5      $	��69* �6   �� 5      $��2* �6   .+�5   
   �� -   5         ��Name$ ExitChatButton��5      $
��Exit��6)   $
��Left=�68   $��585,393,0,0q�6S   $	��Top=�6h   $��31��6�    J� RenderTransformOrigin$ -0.151,0.806&�6�   ��6�   �� *ExitChatButtonCommand+�� 5   
   $��	Send Buzz��6   $
��Left=�6%   $��355,393,0,0q�6@   $	��Top=�6U   $��75��6m   ��6x   �� *
SendBuzz+�� 5   
   $
��Send��6   $
��Left=�6    $��301,393,0,0q�6;   $	��Top=�6P   $��49��6h   $ 
0.35,1.114&�6s   ��6�   �� *SendChatMessage+�� 5   
   $
��Left=�6   $��23��6-   $��	4,390,0,0q�69    �� TextWrapping$
 Wrap=�6L   $	��Top=�6`   $	��292��6x   ��6�   �� *	Message+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5   
   $��	0,0,10,41q�6   $��2��6*   $   tddd49.View.AutoScrollBehavior   
AutoScroll . .��5      �� $	��Top=�6   
�������$	��385��6D   .+�5      �� ��6   �� *Conversation.Messages+��5      �� 5       �� DataType  tddd49.Model.ChatMessage"  6'   .R�5      �� ��6$   �� 5      5      5      5      5      5      