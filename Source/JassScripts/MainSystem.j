globals

    //Constants
    constant integer WES_ABILITY_TYPE_INSTANT 			    = 0
    constant integer WES_ABILITY_TYPE_TARGET  			    = 1
    constant integer WES_ABILITY_TYPE_POINT   			    = 2
    constant integer WES_ABILITY_TYPE_PASSIVE 			    = 3
    constant integer WES_ABILITY_TYPE_PASSIVE_DEALS_DMG     = 4
    constant integer WES_ABILITY_TYPE_PASSIVE_RECEIVED_DMG  = 5
    
    //System
    force WES_callForForce = CreateForce()
    hashtable WES_hash = InitHashtable()
    
    //Spell data
    integer array WES_allocListForAbilityData
    integer array WES_abilityId
    integer array WES_abilityType
    integer array WES_onCast
    
    //Spell data
    integer array WES_allocListForAbilityInstance
    integer array WES_abilityDataForThisInstance
    integer array WES_playerCaster
    integer array WES_caster
    integer array WES_target
    integer array WES_abilityLvl
    
    destructable array WES_targetDestructable
    item array WES_targetItem
    
    real array WES_casterX
    real array WES_casterY
    real array WES_targetX
    real array WES_targetY
    real array WES_casterFacing
    real array WES_targetFacing

endglobals

function Wc3Engine_System_AbilityDataAllocate takes nothing returns integer
    local integer this = WES_allocListForAbilityData[0]
    
    if(0 == WES_allocListForAbilityData[this])then
        set WES_allocListForAbilityData[0] = this+1
    else
        set WES_allocListForAbilityData[0] = WES_allocListForAbilityData[this]
    endif
    
    return this
endfunction

function Wc3Engine_System_AbilityDataDeallocate takes integer this returns nothing
    set WES_allocListForAbilityData[this] = WES_allocListForAbilityData[0]
    set WES_allocListForAbilityData[0] = this
endfunction

function Wc3Engine_System_AbilityCastInstanceAllocate takes nothing returns integer
    local integer this = WES_allocListForAbilityInstance[0]
    
    if(0 == WES_allocListForAbilityInstance[this])then
        set WES_allocListForAbilityInstance[0] = this+1
    else
        set WES_allocListForAbilityInstance[0] = WES_allocListForAbilityInstance[this]
    endif
    
    return this
endfunction

function Wc3Engine_System_AbilityCastInstanceDeallocate takes integer this returns nothing
    set WES_allocListForAbilityInstance[this] = WES_allocListForAbilityInstance[0]
    set WES_allocListForAbilityInstance[0] = this
endfunction


function Wc3Engine_System_LoadInteger takes integer key returns integer
    return LoadInteger(WES_hash, 0, key)
endfunction

function Wc3Engine_System_LoadIntegerStr2D takes string indentifier, integer key returns integer
    return LoadInteger(WES_hash, StringHash(indentifier), key)
endfunction

function Wc3Engine_System_SaveInteger takes integer key, integer value returns nothing
    call SaveInteger(WES_hash, 0, key, value)
endfunction

function Wc3Engine_System_SaveIntegerStr2D takes string indentifier, integer key, integer value returns nothing
    call SaveInteger(WES_hash, StringHash(indentifier), key, value)
endfunction

function Wc3Engine_System_HasInteger takes integer key returns boolean
    return HaveSavedInteger(WES_hash, 0, key)
endfunction

function Wc3Engine_System_DefineAbility takes integer abilityId, integer abilityType, code onCast returns nothing
    local integer this
    
    if ("Default string" == GetObjectName(abilityId)) then
        call DisplayTimedTextToPlayer(GetLocalPlayer(),0,0,60,"[Wc3Engine_System_Debug] ability error: attempted define ability with null id")
        return 
        
    else
        if (Wc3Engine_System_HasInteger(abilityId)) then
            call DisplayTimedTextToPlayer(GetLocalPlayer(),0,0,60,"[Wc3Engine_System_Debug] ability warning: ability id '" + GetObjectName(abilityId) + "' is already registered")
            return
        endif
        
        call Wc3Engine_System_SaveInteger(abilityId, 1)
        
        //Save spell instance into hashtable
        set this = Wc3Engine_System_AbilityDataAllocate()
        call Wc3Engine_System_SaveIntegerStr2D("Ability Data", abilityId, this)
        
        set WES_abilityId[this] = abilityId
        set WES_abilityType[this] = abilityType
        set WES_onCast[this] = C2I(onCast)
    endif
    
endfunction

function Wc3Engine_System_StoreTargetType takes integer this returns nothing
    
    set WES_target[this] = GetUnitId(GetSpellTargetUnit())

    if (null == GetUnitById(WES_target[this])) then
        set WES_targetDestructable[this] = GetSpellTargetDestructable()
        
        if (null == WES_targetDestructable[this]) then
            set WES_targetItem[this] = GetSpellTargetItem()
        endif
    endif
    
    if (null != WES_target[this]) then
        set WES_targetX[this] = GetUnitX(GetUnitById(WES_target[this]))
        set WES_targetY[this] = GetUnitY(GetUnitById(WES_target[this]))
        set WES_casterFacing[this] = GetUnitFacing(GetUnitById(WES_target[this]))
        
    elseif (null != WES_targetDestructable[this]) then
        set WES_targetX[this] = GetDestructableX(WES_targetDestructable[this])
        set WES_targetY[this] = GetDestructableY(WES_targetDestructable[this])
        
    elseif(null != WES_targetItem[this])then
        set WES_targetX[this] = GetItemX(WES_targetItem[this])
        set WES_targetY[this] = GetItemY(WES_targetItem[this])
    endif
    
endfunction

function Wc3Engine_System_OnAbilityCast takes nothing returns nothing
    local integer abilityData = Wc3Engine_System_LoadIntegerStr2D("Ability Data", GetSpellAbilityId())
    local integer this = Wc3Engine_System_AbilityCastInstanceAllocate()

    set WES_abilityDataForThisInstance[this] = abilityData
    set WES_playerCaster[this] = GetPlayerId(GetTriggerPlayer())
    set WES_caster[this] = GetUnitId(GetTriggerUnit())
    set WES_casterX[this] = GetUnitX(GetUnitById(WES_caster[this]))
    set WES_casterY[this] = GetUnitY(GetUnitById(WES_caster[this]))
    set WES_casterFacing[this] = GetUnitFacing(GetUnitById(WES_caster[this]))
    set WES_abilityLvl[this] = GetUnitAbilityLevel(GetUnitById(WES_caster[this]), WES_abilityId[abilityData])
    
    if (WES_abilityType[abilityData] == WES_ABILITY_TYPE_TARGET) then
        call Wc3Engine_System_StoreTargetType(this)
        
    elseif (WES_abilityType[abilityData] == WES_ABILITY_TYPE_POINT) then
        set WES_targetX[this] = GetSpellTargetX()
        set WES_targetY[this] = GetSpellTargetY()
    endif
    
    if (0 != WES_onCast[abilityData]) then
        call ForForce(WES_callForForce, I2C(WES_onCast[abilityData]))
    endif
    
endfunction

function Wc3Engine_MainSystem_OnInit takes nothing returns nothing
    set WES_allocListForAbilityData[0] = 1
    set WES_allocListForAbilityInstance[0] = 1
endfunction