#!/usr/bin/env python
# -*- coding: utf-8 -*-

# from CoreBuild.ServiceItems.AbortProgram.AbortProgramItem import *

from ServiceItems.AbortProgram.AbortProgramItem import *
import glob,os

currentPath = os.getcwd()

serviceRoot = os.path.join(currentPath,"ServiceItems")
serviceCfgSave = os.path.join(currentPath,"SystemConfig")
# 查找ITEM目录
files = os.listdir(serviceRoot)
for file in files:
    # 得到该文件下所有目录的路径
    m = os.path.join(serviceRoot, file)
    # 判断该路径下是否是文件夹
    if (os.path.isdir(m)):
        # itemDir
        itemDir =os.path.join(serviceRoot,file)

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