#region ----------   ABOUT   -----------------------------
"""
##################################################################
# Created By:                                                    #
# Date: 20/01/2015                                               #
# Name: Detect Process                                           #
# Version: 1.0                                                   #
# Windows Tested Versions: Win 7 32-bit                          #
# Python Tested Versions: 2.7 32-bit                             #
# Python Environment  : PyCharm                                  #
##################################################################
"""
#endregion

#region ----------   IMPORTS   -----------------------------
from win32com.client import GetObject
import threading
import os
#endregion


#region -----  CONSTANTS  -----
DEVICE_NAME = "USB Video Device"
HANDLE_EXE_PATH = r"D:\Handle.exe"
THREAD_LIMIT = 200
#endregion

#region ----------   CLASSES   -----------------------------
#region -----  PythonServer CLASS  -----
class  DetectProcess: #(threading.Thread):
    # -----  DATA  -----    ###ME'APES DATA
    physical_device_object_name = None
    process_name = None
    process_id = -1
    status = ""

    # constructor
    #def __init__(self):
        #threading.Thread.__init__(self)

    # the main thread function
    def run(self):
            try:
                print 'Search video device. Please wait..'
                self.wmi = GetObject('winmgmts:')
                self.get_physical_device_object_name()
                # 2 -- Camera connected but not in use  1 -- Camera is on   0 -- Disconnected
                if not self.physical_device_object_name:  #if device not connected
                    self.status = '0'  #return 0
                    return
                processes = self.wmi.InstancesOf('Win32_Process')
                for process in processes:  ##for every process:
                    handlers = self.get_handlers(process.Properties_('Name').Value) ##finds handlers
                    if handlers.find(self.physical_device_object_name) > -1: ##checks if any are the webcam
                        self.process_name = process.Properties_('Name').Value ##if do save process name
                        self.process_id = process.Properties_('ProcessId').Value ##save process id
                        break
                self.status = '1' if self.process_name else '2' ##if the camera is connected but not in a process save status = 2
            except Exception as detail:
                print 'run-time error : ', detail

    def get_handlers( self, processName):##returns handelers for process
         return os.popen(HANDLE_EXE_PATH + " -a -p " + processName).read()

    def get_physical_device_object_name(self): ##finds physical device name for camera (USB video device)
        video_name = None
        for serial in self.wmi.InstancesOf("Win32_PnPSignedDriver"):
            #print (serial.Name, serial.Description)
            if serial.Description and DEVICE_NAME in serial.Description:
                video_name = serial.Description
                break

        if video_name:
            self.physical_device_object_name = os.popen("wmic path Win32_PnPSignedDriver where \"devicename like '" + video_name + "'\" get pdo").read()
            self.physical_device_object_name = self.physical_device_object_name.split('\r\n')[1]
            self.physical_device_object_name = self.physical_device_object_name.strip(' ')
        else:
            self.physical_device_object_name = None

