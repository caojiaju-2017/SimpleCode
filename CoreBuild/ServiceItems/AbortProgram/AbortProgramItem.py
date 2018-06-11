#!/usr/bin/env python
# -*- coding: utf-8 -*-

from CoreBuild.Platform.ItemBase import *

import json

class AbortProgramItem(ItemBase):
    def __init__(self):
        super(AbortProgramItem, self).__init__()
        # self.BaseParam = None
        self.buildConfig()
        pass

    def getCfgJson(self):
        return  self.self_to_json()

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
