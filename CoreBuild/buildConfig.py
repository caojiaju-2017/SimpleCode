#!/usr/bin/env python
# -*- coding: utf-8 -*-

# from CoreBuild.ServiceItems.AbortProgram.AbortProgramItem import *

from ServiceItems.AbortProgram.AbortProgramItem import *
from ServiceItems.ComCommand.ComCommandItem import *
from ServiceItems.Download.DownloadItem import *
from ServiceItems.Excute.ExcuteItem import *
from ServiceItems.GetPixel.GetPixelItem import *
from ServiceItems.LeftMouseClick.LeftMouseClickItem import *
from ServiceItems.LeftMouseDClick.LeftMouseDClickItem import *
from ServiceItems.MoveMouse.MoveMouseItem import *
from ServiceItems.PressKey.PressKeyItem import *
from ServiceItems.RestartComputer.RestartComputerItem import *
from ServiceItems.SendHttpGet.SendHttpGetItem import *
from ServiceItems.SendHttpPost.SendHttpPostItem import *
from ServiceItems.SendMail.SendMailItem import *
from ServiceItems.ShutdownComputer.ShutdownComputerItem import *
from ServiceItems.Snap.SnapItem import *
from ServiceItems.SocketCommand.SocketCommandItem import *
from ServiceItems.StopNet.StopNetItem import *
from ServiceItems.WaitTime.WaitTimeItem import *


from CollectionItems.Array.ArrayItem import *
from CollectionItems.DateTime.DateTimeItem import *
from CollectionItems.Double.DoubleItem import *
from CollectionItems.Int.IntItem import *
from CollectionItems.Json.JsonItem import *
from CollectionItems.String.StringItem import *


from ControlItems.SDCFor.SDCForItem import *
from ControlItems.SDCIf.SDCIfItem import *

import glob,os

currentPath = os.getcwd()

serviceRoot1 = os.path.join(currentPath,"ServiceItems")
serviceRoot2 = os.path.join(currentPath,"CollectionItems")
serviceRoot3 = os.path.join(currentPath,"ControlItems")

serviceCfgSave = os.path.join(currentPath,"SystemConfig")

# 查找ITEM目录
files = os.listdir(serviceRoot1)
for file in files:
    # 得到该文件下所有目录的路径
    m = os.path.join(serviceRoot1, file)
    # 判断该路径下是否是文件夹
    if (os.path.isdir(m)):
        # itemDir
        itemDir =os.path.join(serviceRoot1,file)

        # 配置文件名
        majorClassName = file + "Item"
        itemMajorFile = os.path.join(itemDir,majorClassName + ".py")
        # 判断是否实现了item
        if not os.path.exists(itemMajorFile):
            continue

        print(itemMajorFile)

        # 构造对象
        currentItemInstance = None
        # eval("from CoreBuild.ServiceItems.%s.%sItem import *" % (file,file))

        excuteStr = "%s()" %  majorClassName
        currentItemInstance = eval(excuteStr)
        currentItemInstance.checkResult()
        result = currentItemInstance.getCfgJson()

        # 存储
        cfgFileName = os.path.join(serviceCfgSave, majorClassName + ".setting")

        print (result)
        fileW = open(cfgFileName, 'w')
        fileW.write(result)
        fileW.close()


# 查找ITEM目录
files = os.listdir(serviceRoot2)
for file in files:
    # 得到该文件下所有目录的路径
    m = os.path.join(serviceRoot2, file)
    # 判断该路径下是否是文件夹
    if (os.path.isdir(m)):
        # itemDir
        itemDir =os.path.join(serviceRoot2,file)

        # 配置文件名
        majorClassName = file + "Item"
        itemMajorFile = os.path.join(itemDir,majorClassName + ".py")
        # 判断是否实现了item
        if not os.path.exists(itemMajorFile):
            continue

        print(itemMajorFile)

        # 构造对象
        currentItemInstance = None
        # eval("from CoreBuild.ServiceItems.%s.%sItem import *" % (file,file))

        excuteStr = "%s()" %  majorClassName
        currentItemInstance = eval(excuteStr)
        currentItemInstance.checkResult()
        result = currentItemInstance.getCfgJson()

        # 存储
        cfgFileName = os.path.join(serviceCfgSave, majorClassName + ".setting")

        print (result)
        fileW = open(cfgFileName, 'w')
        fileW.write(result)
        fileW.close()

# 查找ITEM目录
files = os.listdir(serviceRoot3)
for file in files:
    # 得到该文件下所有目录的路径
    m = os.path.join(serviceRoot3, file)
    # 判断该路径下是否是文件夹
    if (os.path.isdir(m)):
        # itemDir
        itemDir =os.path.join(serviceRoot3,file)

        # 配置文件名
        majorClassName = file + "Item"
        itemMajorFile = os.path.join(itemDir,majorClassName + ".py")
        # 判断是否实现了item
        if not os.path.exists(itemMajorFile):
            continue

        print(itemMajorFile)

        # 构造对象
        currentItemInstance = None
        # eval("from CoreBuild.ServiceItems.%s.%sItem import *" % (file,file))

        excuteStr = "%s()" %  majorClassName
        currentItemInstance = eval(excuteStr)
        currentItemInstance.checkResult()
        result = currentItemInstance.getCfgJson()

        # 存储
        cfgFileName = os.path.join(serviceCfgSave, majorClassName + ".setting")

        print (result)
        fileW = open(cfgFileName, 'w')
        fileW.write(result)
        fileW.close()