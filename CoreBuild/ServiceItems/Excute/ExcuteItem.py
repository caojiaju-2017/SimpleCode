#!/usr/bin/env python
# -*- coding: utf-8 -*-

from Platform.ItemBase import *

import json

class ExcuteItem(ItemBase):
    def __init__(self):
        super(ExcuteItem, self).__init__()

        self.itemtype = ItemType.Module
        self.shapetype = ImageShape.Image100004
        self.itemname = "执行外部程序"
        self.iteminfo = "你可以指定需要执行的程序，模块负责执行， 但不负责收集执行结果"
        self.buildConfig()
        pass

    def getCfgJson(self):
        return  self.self_to_json()

    def checkResult(self):
        print("sub class")
    def buildConfig(self):
        '''
        接口初始化函数
        :return:
        '''
        inputS1 = InputBase()
        inputS1.type = InputType.String
        self.setInputDefine("param1", inputS1)

        inputS2 = InputBase()
        inputS2.type = InputType.String
        inputS2.inputIndex = 2
        self.setInputDefine("param2", inputS2)

        outputS = OutputBase()
        outputS.type = InputType.Boolean
        self.setOutput(outputS)
