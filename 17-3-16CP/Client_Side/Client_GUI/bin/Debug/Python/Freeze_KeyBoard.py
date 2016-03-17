import pythoncom, pyHook 

def uMad(event):
    return False

hm = pyHook.HookManager()
hm.MouseAll = uMad
hm.KeyAll = uMad
hm.HookKeyboard()
pythoncom.PumpMessages()
