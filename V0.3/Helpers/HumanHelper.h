// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "UObject/NoExportTypes.h"
#include "Engine/DataTable.h"
#include "Engine/CurveTable.h"
#include "HumanHelper.generated.h"

const FString ToolTypeBattle[] = {FString(TEXT("Infantry")), FString(TEXT("Wizard"))};
const FString ToolTypePeace[] = {FString(TEXT("Collector")), FString(TEXT("Builder"))};

USTRUCT()
struct FHumanSetupInfo
{
    GENERATED_USTRUCT_BODY()

public:
    bool bIsBoy;
    uint8 LegIndex;
    uint8 BodyIndex;
    uint8 HeadIndex;
    uint8 HatIndex;
    uint8 HandIndex;
    bool bIsBattle;
    uint8 ToolTypeIndex;
    uint8 ToolIndex;
    FLinearColor SkinColor;
    FLinearColor BodyColor;
    FLinearColor HatColor;
    FLinearColor ToolColor;
    
};

USTRUCT()
struct FHumanSetupDetail : public FTableRowBase
{
    GENERATED_USTRUCT_BODY()
    
public:
    UPROPERTY() float LegSeparation;
    UPROPERTY() float LegLenght;
    UPROPERTY() float BodyLenght;
    UPROPERTY() float HeadLenght;
    UPROPERTY() float BodyHandHeight;
    UPROPERTY() float BodyWidth;
};

USTRUCT()
struct FHumanActionCurves
{
    GENERATED_USTRUCT_BODY()
    
public:
    FRichCurve* LLegPitch;
    FRichCurve* RLegPitch;
    FRichCurve* BodyRoll;
    FRichCurve* BodyPitch;
    FRichCurve* BodyYaw;
    FRichCurve* LHandAxisYaw;
    FRichCurve* RHandAxisYaw;
    FRichCurve* LHandPitch;
    FRichCurve* RHandPitch;
    FRichCurve* HeadRoll;
    FRichCurve* HeadPitch;
    FRichCurve* HeadYaw;
};

UCLASS()
class PUPPETS_API UHumanHelper : public UObject
{
	GENERATED_BODY()
    
public:
    // Get model path by body part name
    static FString GetModelPath(uint8 PartInedx, FHumanSetupInfo* Info);
    
    // Get head texture path
    static FString GetHeadTexturePath(FHumanSetupInfo* Info);
    
    // Generate setup detail from setup infomation
    static FHumanSetupDetail GetHumanSetupDetail(FHumanSetupInfo* Info);
    
    // Read action curve from curve tables
    static FHumanActionCurves GetHumanActionCurves(const TCHAR* TableText);
    
private:
    // Get name version of index from int with prefix
    static FName NameIndex(FString Prefix, uint8 Index);
    
    // Get string version of index from int
    static FString StringIndex(uint8 Index);
    
};
