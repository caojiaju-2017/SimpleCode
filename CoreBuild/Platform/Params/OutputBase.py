#!/usr/bin/env python
# -*- coding: utf-8 -*-

from CoreBuild.Platform.Params.InputType import *

class OutputBase(object):
    '''
    输出参数类型
    '''
    def __init__(self):
        self.value = None
        self.type = InputType.String
