// Fill out your copyright notice in the Description page of Project Settings.

#include "HumanPawn.h"

// Sets default values
AHumanPawn::AHumanPawn()
{
 	// Set this pawn to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;
    
    TrueRoot = CreateDefaultSubobject<USceneComponent>(TEXT("TrueRoot"));
    SetRootComponent(TrueRoot);
    
    BodyRoot = CreateDefaultSubobject<USceneComponent>(TEXT("BodyRoot"));
    BodyRoot->AttachToComponent(TrueRoot,FAttachmentTransformRules::KeepRelativeTransform);
    
    LLeg = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("LLeg"));
    LLeg->AttachToComponent(BodyRoot,FAttachmentTransformRules::KeepRelativeTransform);
    
    RLeg = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("RLeg"));
    RLeg->AttachToComponent(BodyRoot,FAttachmentTransformRules::KeepRelativeTransform);
    
    Body = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Body"));
    Body->AttachToComponent(BodyRoot,FAttachmentTransformRules::KeepRelativeTransform);
    
    Head = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Head"));
    Head->AttachToComponent(Body,FAttachmentTransformRules::KeepRelativeTransform);
    
    Hat = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Hat"));
    Hat->AttachToComponent(Head,FAttachmentTransformRules::KeepRelativeTransform);
    
    LHandAxis = CreateDefaultSubobject<USceneComponent>(TEXT("LHandAxis"));
    LHandAxis->AttachToComponent(Body,FAttachmentTransformRules::KeepRelativeTransform);
    
    RHandAxis = CreateDefaultSubobject<USceneComponent>(TEXT("RHandAxis"));
    RHandAxis->AttachToComponent(Body,FAttachmentTransformRules::KeepRelativeTransform);
    
    LHand = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("LHand"));
    LHand->AttachToComponent(LHandAxis,FAttachmentTransformRules::KeepRelativeTransform);
    
    RHand = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("RHand"));
    RHand->AttachToComponent(RHandAxis,FAttachmentTransformRules::KeepRelativeTransform);
    
    LTool = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("LTool"));
    LTool->AttachToComponent(LHand,FAttachmentTransformRules::KeepRelativeTransform);
    
    RTool = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("RTool"));
    RTool->AttachToComponent(RHand,FAttachmentTransformRules::KeepRelativeTransform);
    
    ActionPointer = 0;
    ActionSpeed = 1;
    ActionRemain = 0;
    WalkRemain = -1;
    RotateRemain = -1;
    RotateSpeed = 30;
}

// Called when the game starts or when spawned
void AHumanPawn::BeginPlay()
{
	Super::BeginPlay();
}

// Called every frame
void AHumanPawn::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
    
    ActionPointer += DeltaTime * ActionSpeed;
    if(ActionPointer >= 1)
    {
        ActionPointer -= FMath::RoundToZero(ActionPointer);
        if(ActionRemain <= 0) HumanActionCurves = UHumanHelper::GetHumanActionCurves(TEXT("Idle"));
    }
    UpdateAction(DeltaTime);
}

void AHumanPawn::Setup(FHumanSetupInfo* Info)
{
    HumanSetupInfo = *Info;
    HumanSetupDetail = UHumanHelper::GetHumanSetupDetail(Info);
    SetupMeshs();
    SetupMaterials();
    HumanActionCurves = UHumanHelper::GetHumanActionCurves(TEXT("Idle"));
}

void AHumanPawn::CommandReset()
{
    HumanActionCurves = UHumanHelper::GetHumanActionCurves(TEXT("Idle"));
    ActionRemain = 0;
    WalkRemain = -1;
    RotateRemain = 0;
}

void AHumanPawn::CommandWalk(float Distance)
{
    HumanActionCurves = UHumanHelper::GetHumanActionCurves(TEXT("Walk"));
    ActionPointer = 0;
    ActionSpeed = 1;
    ActionRemain = 1;
    WalkRemain = Distance;
}

void AHumanPawn::CommandRotate(float Degree)
{
    RotateRemain = Degree;
    RotateSpeed = 30;
}

void AHumanPawn::SetupMeshs()
{
    LLeg->SetStaticMesh(Cast<UStaticMesh>(StaticLoadObject(UStaticMesh::StaticClass(),NULL,*UHumanHelper::GetModelPath(0, &HumanSetupInfo))));
    LLeg->SetRelativeLocation(FVector(0, HumanSetupDetail.LegSeparation, HumanSetupDetail.LegLenght));
    
    RLeg->SetStaticMesh(Cast<UStaticMesh>(StaticLoadObject(UStaticMesh::StaticClass(),NULL,*UHumanHelper::GetModelPath(0, &HumanSetupInfo))));
    RLeg->SetRelativeLocation(FVector(0, -HumanSetupDetail.LegSeparation, HumanSetupDetail.LegLenght));
    
    Body->SetStaticMesh(Cast<UStaticMesh>(StaticLoadObject(UStaticMesh::StaticClass(),NULL,*UHumanHelper::GetModelPath(1, &HumanSetupInfo))));
    Body->SetRelativeLocation(FVector(0, 0, HumanSetupDetail.LegLenght));
    
    Head->SetStaticMesh(Cast<UStaticMesh>(StaticLoadObject(UStaticMesh::StaticClass(),NULL,*UHumanHelper::GetModelPath(2, &HumanSetupInfo))));
    Head->SetRelativeLocation(FVector(0, 0, HumanSetupDetail.BodyLenght));
    
    Hat->SetStaticMesh(Cast<UStaticMesh>(StaticLoadObject(UStaticMesh::StaticClass(),NULL,*UHumanHelper::GetModelPath(3, &HumanSetupInfo))));
    Hat->SetRelativeLocation(FVector(0, 0, HumanSetupDetail.HeadLenght));
    
    LHandAxis->SetRelativeLocation(FVector(0, 0, HumanSetupDetail.BodyHandHeight));
    
    RHandAxis->SetRelativeLocation(FVector(0, 0, HumanSetupDetail.BodyHandHeight));
    
    LHand->SetStaticMesh(Cast<UStaticMesh>(StaticLoadObject(UStaticMesh::StaticClass(),NULL,*UHumanHelper::GetModelPath(4, &HumanSetupInfo))));
    LHand->SetRelativeLocation(FVector(0, HumanSetupDetail.BodyWidth, 0));
    
    RHand->SetStaticMesh(Cast<UStaticMesh>(StaticLoadObject(UStaticMesh::StaticClass(),NULL,*UHumanHelper::GetModelPath(4, &HumanSetupInfo))));
    RHand->SetRelativeLocation(FVector(0, -HumanSetupDetail.BodyWidth, 0));
    
    LTool->SetStaticMesh(Cast<UStaticMesh>(StaticLoadObject(UStaticMesh::StaticClass(),NULL,*UHumanHelper::GetModelPath(5, &HumanSetupInfo))));
    
    RTool->SetStaticMesh(Cast<UStaticMesh>(StaticLoadObject(UStaticMesh::StaticClass(),NULL,*UHumanHelper::GetModelPath(6, &HumanSetupInfo))));
}

void AHumanPawn::SetupMaterials()
{
    LinkMaterialBase(LLeg, FString(TEXT("Body_Mat_Base")));
    LinkMaterialBase(RLeg, FString(TEXT("Body_Mat_Base")));
    LinkMaterialBase(Body, FString(TEXT("Body_Mat_Base")));
    LinkMaterialBase(Head, FString(TEXT("Head_Mat_Base")));
    LinkMaterialBase(Hat, FString(TEXT("Body_Mat_Base")));
    LinkMaterialBase(LHand, FString(TEXT("Body_Mat_Base")));
    LinkMaterialBase(RHand, FString(TEXT("Body_Mat_Base")));
    LinkMaterialBase(LTool, FString(TEXT("Body_Mat_Base")));
    LinkMaterialBase(RTool, FString(TEXT("Body_Mat_Base")));

    LLeg->CreateAndSetMaterialInstanceDynamic(0)->SetVectorParameterValue(FName("BaseColor"), HumanSetupInfo.SkinColor);
    RLeg->CreateAndSetMaterialInstanceDynamic(0)->SetVectorParameterValue(FName("BaseColor"), HumanSetupInfo.SkinColor);
    Body->CreateAndSetMaterialInstanceDynamic(0)->SetVectorParameterValue(FName("BaseColor"), HumanSetupInfo.BodyColor);
    
    UMaterialInstanceDynamic* HeadMaterialInstance = Head->CreateAndSetMaterialInstanceDynamic(0);
    HeadMaterialInstance->SetVectorParameterValue(FName("BaseColor"), HumanSetupInfo.SkinColor);
    UTexture* HeadTexture = Cast<UTexture>(StaticLoadObject(UTexture::StaticClass(),NULL,*UHumanHelper::GetHeadTexturePath(&HumanSetupInfo)));
    HeadMaterialInstance->SetTextureParameterValue(FName("BaseTexture"), HeadTexture);
    
    Hat->CreateAndSetMaterialInstanceDynamic(0)->SetVectorParameterValue(FName("BaseColor"), HumanSetupInfo.HatColor);
    LHand->CreateAndSetMaterialInstanceDynamic(0)->SetVectorParameterValue(FName("BaseColor"), HumanSetupInfo.SkinColor);
    RHand->CreateAndSetMaterialInstanceDynamic(0)->SetVectorParameterValue(FName("BaseColor"), HumanSetupInfo.SkinColor);
    LTool->CreateAndSetMaterialInstanceDynamic(0)->SetVectorParameterValue(FName("BaseColor"), HumanSetupInfo.ToolColor);
    RTool->CreateAndSetMaterialInstanceDynamic(0)->SetVectorParameterValue(FName("BaseColor"), HumanSetupInfo.ToolColor);
}

void AHumanPawn::LinkMaterialBase(UStaticMeshComponent* Component, FString MaterialString)
{
    FString MaterialPath = FString(TEXT("/Game/Materials_Human/")) + MaterialString;
    Component->SetMaterial(0, Cast<UMaterialInterface>(StaticLoadObject(UMaterialInterface::StaticClass(),NULL,*MaterialPath)));
}

void AHumanPawn::UpdateAction(float DeltaTime)
{
    float Sin, Cos;
    float DeltaLLegX, DeltaRLegX;
    float WalkLength, RotateAngle;
    if(WalkRemain > 0)
    {
        FMath::SinCos(&Sin, &Cos, FMath::DegreesToRadians(LLeg->RelativeRotation.Pitch));
        DeltaLLegX = HumanSetupDetail.LegLenght * Sin;
        FMath::SinCos(&Sin, &Cos, FMath::DegreesToRadians(RLeg->RelativeRotation.Pitch));
        DeltaRLegX = HumanSetupDetail.LegLenght * Sin;
    }
    
    LLeg->SetRelativeRotation(FRotator(HumanActionCurves.LLegPitch->Eval(ActionPointer),0,0));
    RLeg->SetRelativeRotation(FRotator(HumanActionCurves.RLegPitch->Eval(ActionPointer),0,0));
    Body->SetRelativeRotation(FRotator(HumanActionCurves.BodyPitch->Eval(ActionPointer),HumanActionCurves.BodyYaw->Eval(ActionPointer),HumanActionCurves.BodyRoll->Eval(ActionPointer)));
    LHandAxis->SetRelativeRotation(FRotator(0,HumanActionCurves.LHandAxisYaw->Eval(ActionPointer),0));
    RHandAxis->SetRelativeRotation(FRotator(0,HumanActionCurves.RHandAxisYaw->Eval(ActionPointer),0));
    LHand->SetRelativeRotation(FRotator(HumanActionCurves.LHandPitch->Eval(ActionPointer),0,0));
    RHand->SetRelativeRotation(FRotator(HumanActionCurves.RHandPitch->Eval(ActionPointer),0,0));
    Head->SetRelativeRotation(FRotator(HumanActionCurves.HeadPitch->Eval(ActionPointer),HumanActionCurves.HeadYaw->Eval(ActionPointer),HumanActionCurves.HeadRoll->Eval(ActionPointer)));
    
    if(WalkRemain > 0)
    {
        FMath::SinCos(&Sin, &Cos, FMath::DegreesToRadians(LLeg->RelativeRotation.Pitch));
        DeltaLLegX = HumanSetupDetail.LegLenght * Sin - DeltaLLegX;
        FMath::SinCos(&Sin, &Cos, FMath::DegreesToRadians(RLeg->RelativeRotation.Pitch));
        DeltaRLegX = HumanSetupDetail.LegLenght * Sin - DeltaRLegX;
        
        WalkLength = DeltaLLegX > DeltaRLegX ? DeltaLLegX : DeltaRLegX;
        FMath::SinCos(&Sin, &Cos, FMath::DegreesToRadians(GetActorRotation().Yaw));
        SetActorLocation(GetActorLocation() + FVector(-WalkLength * Cos, -WalkLength * Sin, 0));
        
        WalkRemain -= WalkLength;
        if(WalkRemain <= 0) ActionRemain = 0;
    }
    
    if(!FMath::IsNearlyZero(RotateRemain, RotateSpeed * DeltaTime))
    {
        RotateAngle = RotateRemain > 0 ? RotateSpeed * DeltaTime : -RotateSpeed * DeltaTime;
        SetActorRotation(GetActorRotation() + FRotator(0, -RotateAngle, 0));
        RotateRemain -= RotateAngle;
        RotateRemain = FMath::UnwindDegrees(RotateRemain);
    }
    
    FMath::SinCos(&Sin, &Cos, FMath::DegreesToRadians(LLeg->RelativeRotation.Pitch));
    BodyRoot->SetRelativeLocation(FVector(0,0,HumanSetupDetail.LegLenght * (Cos - 1)));
}
