#!/usr/bin/env python
# -*- coding: utf-8 -*-

from Platform.ItemBase import *

import json

class GetPixelItem(ItemBase):
    def __init__(self):
        super(GetPixelItem, self).__init__()

        self.itemtype = ItemType.Module
        self.shapetype = ImageShape.Image100005
        self.itemname = "获取像素"
        self.iteminfo = "模块支持后去指定坐标的像素值"
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
