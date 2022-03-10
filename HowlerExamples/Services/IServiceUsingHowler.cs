﻿using HowlerExamples.Models;
using HowlerExamples.Structures;

namespace HowlerExamples.Services;

public interface IServiceUsingHowler
{
    string GetData();
    string GetMoreData();
    string PostData(Dto dto);
    string PostDataGenerics(DtoGeneric dto);
}