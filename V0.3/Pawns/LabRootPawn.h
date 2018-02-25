// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/DefaultPawn.h"
#include "Engine/World.h"
#include "Pawns/HumanPawn.h"
#include "Helpers/HumanHelper.h"
#include "LabRootPawn.generated.h"

UCLASS()
class PUPPETS_API ALabRootPawn : public ADefaultPawn
{
	GENERATED_BODY()
    
protected:
    // Called when the game starts or when spawned
    virtual void BeginPlay() override;
    
public:
    UFUNCTION(BlueprintCallable, Category="Action")
    void HumanIdle();
    
    UFUNCTION(BlueprintCallable, Category="Action")
    void HumanWalk();
    
    UFUNCTION(BlueprintCallable, Category="Action")
    void HumanRotate();
    
private:
    AHumanPawn* BoyPawn;
    AHumanPawn* GirlPawn;
};
