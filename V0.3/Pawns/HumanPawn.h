// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Pawn.h"
#include "Engine/StaticMesh.h"
#include "Components/StaticMeshComponent.h"
#include "Materials/MaterialInstanceDynamic.h"
#include "Helpers/HumanHelper.h"
#include "HumanPawn.generated.h"

UCLASS()
class PUPPETS_API AHumanPawn : public APawn
{
	GENERATED_BODY()

public:
	// Sets default values for this pawn's properties
	AHumanPawn();

protected:
	// Called when the game starts or when spawned
	virtual void BeginPlay() override;

public:	
	// Called every frame
	virtual void Tick(float DeltaTime) override;

public:
    // Called by the Pawn's owner for initiate
    void Setup(FHumanSetupInfo* Info);
    
    // Action Commands
    void CommandReset();
    void CommandWalk(float Distance);
    void CommandRotate(float Degree);
    
private:
    void SetupMeshs();
    void SetupMaterials();
    void LinkMaterialBase(UStaticMeshComponent* Component, FString MaterialString);
    void UpdateAction(float DeltaTime);
    
private:
    // Body parts
    USceneComponent* TrueRoot;
    USceneComponent* BodyRoot;
    UStaticMeshComponent* LLeg;
    UStaticMeshComponent* RLeg;
    UStaticMeshComponent* Body;
    UStaticMeshComponent* Head;
    UStaticMeshComponent* Hat;
    USceneComponent* LHandAxis;
    USceneComponent* RHandAxis;
    UStaticMeshComponent* LHand;
    UStaticMeshComponent* RHand;
    UStaticMeshComponent* LTool;
    UStaticMeshComponent* RTool;
    // Human setup information
    FHumanSetupInfo HumanSetupInfo;
    FHumanSetupDetail HumanSetupDetail;
    // Human action related
    FHumanActionCurves HumanActionCurves;
    float ActionPointer;
    float ActionSpeed;
    uint8 ActionRemain;
    float WalkRemain;
    float RotateRemain;
    float RotateSpeed;
	
};
