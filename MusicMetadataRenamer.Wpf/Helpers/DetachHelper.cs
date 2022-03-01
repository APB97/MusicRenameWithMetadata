using System.Windows;
using System.Windows.Controls;

namespace MusicMetadataRenamer.Wpf.Helpers
{
    public static class DetachHelper
    {
        public static void DetachChild(this DependencyObject parent, UIElement uiElement)
        {
            switch (parent)
            {
                case Panel panel:
                    panel.Children.Remove(uiElement);
                    break;
                case Decorator decorator:
                    if (decorator.Child == uiElement)
                        decorator.Child = null;
                    break;
                case ContentPresenter presenter:
                    if (ReferenceEquals(presenter.Content, uiElement))
                        presenter.Content = null;
                    break;
                case ContentControl control:
                    if (ReferenceEquals(control.Content, uiElement))
                        control.Content = null;
                    break;
            }
        }
    }
}