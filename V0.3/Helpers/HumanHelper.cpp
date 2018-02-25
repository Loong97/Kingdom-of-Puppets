// Fill out your copyright notice in the Description page of Project Settings.

#include "HumanHelper.h"

FString UHumanHelper::GetModelPath(uint8 PartInedx, FHumanSetupInfo* Info)
{
    FString BasePath = FString(TEXT("/Game/Models_Human/"));
    FString GenderString = Info->bIsBoy ? FString(TEXT("Boy")) : FString(TEXT("Girl"));
    FString UDL = FString(TEXT("_"));
    
    switch (PartInedx) {
        case 0:
            return BasePath + FString(TEXT("Leg")) + UDL + GenderString + UDL + StringIndex(Info->LegIndex);
            break;
        case 1:
            return BasePath + FString(TEXT("Body")) + UDL + GenderString + UDL + StringIndex(Info->BodyIndex);
            break;
        case 2:
            return BasePath + FString(TEXT("Head")) + UDL + GenderString + UDL + StringIndex(Info->HeadIndex);
            break;
        case 3:
            return BasePath + FString(TEXT("Hat")) + UDL + GenderString + UDL + StringIndex(Info->HatIndex);
            break;
        case 4:
            return BasePath + FString(TEXT("Hand")) + UDL + GenderString + UDL + StringIndex(Info->HandIndex);
            break;
        case 5:
            return Info->bIsBattle ?
            BasePath + FString(TEXT("Armour")) + UDL + ToolTypeBattle[Info->ToolTypeIndex] + UDL + StringIndex(Info->ToolIndex) + UDL + FString(TEXT("L")) :
            BasePath + FString(TEXT("Tool")) + UDL + ToolTypePeace[Info->ToolTypeIndex] + UDL + StringIndex(Info->ToolIndex) + UDL + FString(TEXT("L"));
            break;
        case 6:
            return Info->bIsBattle ?
            BasePath + FString(TEXT("Armour")) + UDL + ToolTypeBattle[Info->ToolTypeIndex] + UDL + StringIndex(Info->ToolIndex) + UDL + FString(TEXT("R")) :
            BasePath + FString(TEXT("Tool")) + UDL + ToolTypePeace[Info->ToolTypeIndex] + UDL + StringIndex(Info->ToolIndex) + UDL + FString(TEXT("R"));
            break;
        default:
            return FString(TEXT("Unknown Part Index"));
            break;
    }
}

FString UHumanHelper::GetHeadTexturePath(FHumanSetupInfo* Info)
{
    FString BasePath = FString(TEXT("/Game/Materials_Human/Head_"));
    FString GenderString = Info->bIsBoy ? FString(TEXT("Boy")) : FString(TEXT("Girl"));
    FString UDL = FString(TEXT("_"));
    
    return BasePath + GenderString + UDL + StringIndex(Info->HeadIndex);
}

FHumanSetupDetail UHumanHelper::GetHumanSetupDetail(FHumanSetupInfo* Info)
{
    FHumanSetupDetail SetupDetail;
    UDataTable* Table;
    Table = Cast<UDataTable>(StaticLoadObject(UDataTable::StaticClass(),NULL,TEXT("/Game/Charts/Human_Setup_Detail")));
    FString GenderString = Info->bIsBoy ? FString(TEXT("B")) : FString(TEXT("G"));
    
    SetupDetail.LegSeparation = Table->FindRow<FHumanSetupDetail>(NameIndex(GenderString,Info->LegIndex),TEXT("LookupRow"))->LegSeparation;
    SetupDetail.LegLenght = Table->FindRow<FHumanSetupDetail>(NameIndex(GenderString,Info->LegIndex),TEXT("LookupRow"))->LegLenght;
    SetupDetail.BodyLenght = Table->FindRow<FHumanSetupDetail>(NameIndex(GenderString,Info->BodyIndex),TEXT("LookupRow"))->BodyLenght;
    SetupDetail.HeadLenght = Table->FindRow<FHumanSetupDetail>(NameIndex(GenderString,Info->HeadIndex),TEXT("LookupRow"))->HeadLenght;
    SetupDetail.BodyHandHeight = Table->FindRow<FHumanSetupDetail>(NameIndex(GenderString,Info->BodyIndex),TEXT("LookupRow"))->BodyHandHeight;
    SetupDetail.BodyWidth = Table->FindRow<FHumanSetupDetail>(NameIndex(GenderString,Info->BodyIndex),TEXT("LookupRow"))->BodyWidth;
    
    return SetupDetail;
}

FHumanActionCurves UHumanHelper::GetHumanActionCurves(const TCHAR* TableText)
{
    FHumanActionCurves ActionCurves;
    UCurveTable* Table;
    FString TablePath = FString(TEXT("/Game/Charts/Human_Anim_")) + FString(TableText);
    Table = Cast<UCurveTable>(StaticLoadObject(UCurveTable::StaticClass(),NULL,*TablePath));
    
    ActionCurves.LLegPitch = Table->FindCurve(FName(TEXT("LLegPitch")),TEXT("LookupRow"));
    ActionCurves.RLegPitch = Table->FindCurve(FName(TEXT("RLegPitch")),TEXT("LookupRow"));
    ActionCurves.BodyRoll = Table->FindCurve(FName(TEXT("BodyRoll")),TEXT("LookupRow"));
    ActionCurves.BodyPitch = Table->FindCurve(FName(TEXT("BodyPitch")),TEXT("LookupRow"));
    ActionCurves.BodyYaw = Table->FindCurve(FName(TEXT("BodyYaw")),TEXT("LookupRow"));
    ActionCurves.LHandAxisYaw = Table->FindCurve(FName(TEXT("LHandAxisYaw")),TEXT("LookupRow"));
    ActionCurves.RHandAxisYaw = Table->FindCurve(FName(TEXT("RHandAxisYaw")),TEXT("LookupRow"));
    ActionCurves.LHandPitch = Table->FindCurve(FName(TEXT("LHandPitch")),TEXT("LookupRow"));
    ActionCurves.RHandPitch = Table->FindCurve(FName(TEXT("RHandPitch")),TEXT("LookupRow"));
    ActionCurves.HeadRoll = Table->FindCurve(FName(TEXT("HeadRoll")),TEXT("LookupRow"));
    ActionCurves.HeadPitch = Table->FindCurve(FName(TEXT("HeadPitch")),TEXT("LookupRow"));
    ActionCurves.HeadYaw = Table->FindCurve(FName(TEXT("HeadYaw")),TEXT("LookupRow"));
    
    return ActionCurves;
}

FName UHumanHelper::NameIndex(FString Prefix, uint8 Index)
{
    FString String = Prefix + StringIndex(Index);
    return FName(*String);
}

FString UHumanHelper::StringIndex(uint8 Index)
{
    FString BaseString = FString::FromInt((int32)Index);
    
    if(0 <= Index && Index < 10) return FString(TEXT("00")) + BaseString;
    else if(10 <= Index && Index < 100) return FString(TEXT("0")) + BaseString;
    else return BaseString;
}
