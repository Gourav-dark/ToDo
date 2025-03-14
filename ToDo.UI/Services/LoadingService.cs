﻿namespace ToDo.UI.Services;
public class LoadingService
{
    public event Action OnShow;
    public event Action OnHide;

    public void Show()
    {
        OnShow?.Invoke();
    }

    public void Hide()
    {
        OnHide?.Invoke();
    }
}
