module Farmer.Arm.ApplicationGateway

open Farmer
open Farmer.ApplicationGateway

let ApplicationGateways = ResourceType ("Microsoft.Network/applicationGateways", "2020-11-01")
let ApplicationGatewayFrontendIPConfigurations = ResourceType ("Microsoft.Network/applicationGateways/frontendIPConfigurations", "2020-11-01")
let ApplicationGatewayFrontendPorts = ResourceType ("Microsoft.Network/applicationGateways/frontendPorts", "2020-11-01")
let ApplicationGatewayHttpListeners = ResourceType ("Microsoft.Network/applicationGateways/httpListeners", "2020-11-01")
let ApplicationGatewayBackendHttpSettingsCollection = ResourceType ("Microsoft.Network/applicationGateways/backendHttpSettingsCollection", "2020-11-01")
let ApplicationGatewayBackendAddressPools = ResourceType ("Microsoft.Network/applicationGateways/backendAddressPools", "2020-11-01")
let ApplicationGatewayProbes = ResourceType ("Microsoft.Network/applicationGateways/probes", "2020-11-01")
let ApplicationGatewayRequestRoutingRules = ResourceType ("Microsoft.Network/applicationGateways/requestRoutingRules", "2020-11-01")


type ApplicationGateway =
    { Name : ResourceName
      Location : Location
      Sku : ApplicationGatewaySku
      AuthenticationCertificates:
        {| Name: ResourceName
           Data: string |} list
      AutoscaleConfiguration:
        {| MaxCapacity: int
           MinCapacity: int |}
      FrontendPorts : 
        {| Name : ResourceName
           Port : uint16 |} list
      FrontendIpConfigs :
        {|  Name : ResourceName
            PrivateIpAllocationMethod : PrivateIpAddress.AllocationMethod
            PublicIp : ResourceId option |} list
      BackendAddressPools : 
        {| Name : ResourceName
           BackendAddresses: 
            {| Fqdn: string
               IpAddress: string |} list |} list
      BackendHttpSettingsCollection : 
        {| Name: ResourcName
           AffinityCookieName: string
           AuthenticationCertificates: ResourceName list
           ConnectionDraining: {|
             DrainTimeoutInSeconds: int<Seconds>
             Enabled: bool
           |}
           CookieBasedAffinity: FeatureFlag
           HostName: string
           Path: string
           Port: unit16
           Protocol: Protocol
           CookieBasedAffinity: FeatureFlag
           PickHostNameFromBackendAddress: bool
           RequestTimeoutInSeconds: int<Seconds>
           Probe: ResourceName list
           TrustedRootCertificates : ResourceName list |} list
      CustomErrorConfigurations:
          {| CustomErrorPageUrl: string
             StatusCode: string |} list
      EnableFips: bool
      EnableHttp2: bool
      FirewallPolicy: ResourceName
      ForceFirewallPolicyAssociation: bool
      GatewayIPConfigurations:
          {| Name: ResourceName
             Subnet: ResourceName |} list
      HttpListeners :
          {|  /// Name of the listener
              Name : ResourceName
              FrontendIpConfiguration : ResourceName
              BackendAddressPool : ResourceName
              CustomErrorConfigurations:  
                {| CustomErrorPageUrl: string
                   SstatusCode: string |} list
              FirewallPolicy : ResourceName
              FrontendPort : ResourceName
              RequireServerNameIndication : bool
              HostNames : string list
              Protocol : Protocol
              SslCertificate : ResourceName
              SslProfile : ResourceName
          |} list
      Probes :
          {|  /// Name of the probe
              Name : ResourceName
              /// Protocol
              Protocol : Protocol
              IntervalInSeconds : int<Seconds>
              TimeoutInSeconds : int<Seconds>
              UnhealthyThreshold : uint16
              PickHostNameFromBackendHttpSettings : bool
              MinServers : uint16
              Match : {|
                  Body: string
                  StatusCodes: strubg list
              |}
          |} list
      RedirectConfigurations:
        {| Name: ResourceName
           IncludePath: bool
           IncludeQueryString: bool
           PathRules: ResourceName list
           RedirectType: RedirectType
           RequestRoutingRules: ResourceName list
           TargetListener: ResourceName
           TargetUrl: string
           UrlPathMaps: ResourceName list |} list
      RequestRoutingRules : 
        {|  Name: ResourceName,
            RuleType: RuleType,
            HttpListener: ResourceName,
            BackendAddressPool: ResourceName,
            BackendHttpSettings: ResourceName
            RedirectConfiguration: ResourceName
            RewriteRuleSet: ResourceName
            UrlPathMap: ResourceName
            Priority: int //?
        |} list
      RewriteRuleSets: 
        {|  Name: ResourceName
            RewriteRules:
              {| ActionSet: 
                  {| RequestHeaderConfigurations:
                       {| HeaderName: string
                          HeaderValue: string |} list 
                     ResponseHeaderConfigurations:
                       {| HeaderName: string
                          HeaderValue: string |} list
                     UrlConfiguration:
                       {| ModifiedPath: string
                          ModifiedQueryString: string
                          Reroute: bool |} |}
                 Conditions:
                  {| IgnoreCase: bool
                     Negate: bool
                     Pattern: string
                     Variable: string |} list
                 Name: string
                 RuleSequence: int |} list |}
      SslCertificates:
        {| Name: ResourceName
           Data: string
           KeyVaultSecretId: string
           Password: string |} list
      SslPolicy: {|
          CipherSuites: string list
          DisabledSslProtocols: string list
          MinProtocolVersion: string
          PolicyName: string
          PolicyType: string
      |}
      SslProfiles:
          {| Name: ResourceName
             ClientAuthConfiguration: 
               {| VerifyClientCertIssuerDN: bool |}
             SslPolicy: 
               {| CipherSuites: string list
                  DisabledSslProtocols: string list
                  MinProtocolVersion: string
                  PolicyName: string
                  PolicyType: string |}
             TrustedClientCertificates: ResourceName list
          |} list
      TrustedClientCertificates:
          {| Name: ResourceName
             Data: string |} list
      TrustedRootCertificates:
          {| Name: ResourceName
             Data: string
             KeyVaultSecretId: string |} list
      UrlPathMaps:
          {| Name: ResourceName
             DefaultBackendAddressPool: ResourceName
             DefaultBackendHttpSettings: ResourceName
             DefaultRedirectConfiguration: ResourceName
             DefaultRewriteRuleSet: ResourceName
             PathRules: 
              {|
                  Name: ResourceName
                  BackendAddressPool: ResourceName
                  BackendHttpSettings: ResourceName
                  FirewallPolicy: ResourceName
                  Paths: string list
                  RedirectConfiguration: ResourceName
                  RewriteRuleSet: ResourceName
              |} list
          |} list
      WebApplicationFirewallConfiguration:
          {| DisabledRuleGroups:
              {| RuleGroupName: string
                 Rules: [ int ] |} list
             Enabled: bool
             Exclusions:
             {|  MatchVariable: string
                 Selector: string
                 SelectorMatchOperator: string |} list
             FileUploadLimitInMb: int<Mb>
             FirewallMode: FirewallMode
             // MaxRequestBodySize: int // ??
             MaxRequestBodySizeInKb: int<Kb>
             RequestBodyCheck: bool
             RuleSetType: RuleSetType
             RuleSetVersion: string |}
      Zones: string list
      Dependencies: Set<ResourceId>
      Tags: Map<string,string> }
    interface IArmResource with
        member this.ResourceId = ApplicationGateways.resourceId this.Name
        member this.JsonModel =
            {| ApplicationGateways.Create (this.Name, this.Location, this.Dependencies, this.Tags) with
                sku =
                    {|
                        name = this.Sku.Name.ArmValue
                        capacity = this.Sku.Capacity.ArmValue
                        tier = this.Sku.Tier.ArmValue
                    |}
                properties =
                    {|
                        frontendPorts = this.FrontendIpConfigs |> List.map (fun frontend ->
                            let allocationMethod, ip =
                                match frontend.PrivateIpAllocationMethod with
                                    | PrivateIpAddress.DynamicPrivateIp -> "Dynamic", null
                                    | PrivateIpAddress.StaticPrivateIp ip -> "Static", string ip
                            {| name = frontend.Name.Value
                               properties =
                                   {|  privateIPAllocationMethod = allocationMethod
                                       privateIPAddress = ip
                                       publicIPAddress =
                                           frontend.PublicIp |> Option.map (fun pip -> {| id = pip.Eval() |} )
                                           |> Option.defaultValue Unchecked.defaultof<_>
                                   |}
                            |}
                        )
                        backendAddressPools = this.BackendAddressPools |> List.map (fun backend ->
                            {| name = backend.Value |}
                        )
                        probes = this.Probes |> List.map (fun probe ->
                            {|
                                name = probe.Name.Value
                                properties =
                                    {|
                                        protocol = probe.Protocol.ArmValue
                                        port = probe.Port
                                        requestPath = probe.RequestPath
                                        intervalInSeconds = probe.IntervalInSeconds
                                        numberOfProbes = probe.NumberOfProbes
                                    |}
                            |}
                        )
                    |}
            |} :> _

type BackendAddressPool =
    {   /// Name of the backend address pool
        Name : ResourceName
        /// Name of the load balancer where this pool will be added.
        ApplicationGateway : ResourceName
        /// Addresses of backend services.
        ApplicationGatewayBackendAddresses :
            {|  /// Unique name for the backend address
                Name : ResourceName
                /// Resource ID of a virtual network where the backend IP can be found.
                VirtualNetwork : LinkedResource option
                /// IP Address of the backend resource in the pool
                IpAddress : System.Net.IPAddress
            |} list
    }
    interface IArmResource with
        member this.ResourceId = ApplicationGatewayBackendAddressPools.resourceId (this.ApplicationGateway, this.Name)
        member this.JsonModel =
            let dependencies =
                seq {
                    yield ApplicationGateways.resourceId this.ApplicationGateway
                    for addr in this.ApplicationGatewayBackendAddresses do
                        match addr.VirtualNetwork with
                        | Some (Managed vnetId) -> yield vnetId
                        | _ -> ()
                } |> Set.ofSeq
            {| ApplicationGatewayBackendAddressPools.Create(this.Name, dependsOn=dependencies) with
                name = $"{this.ApplicationGateway.Value}/{this.Name.Value}"
                properties =
                    {| ApplicationGatewayBackendAddresses = this.ApplicationGatewayBackendAddresses |> List.map (fun addr ->
                        {|  name = addr.Name.Value
                            properties =
                                {| ipAddress = string addr.IpAddress
                                   virtualNetwork =
                                       match addr.VirtualNetwork with
                                       | Some (Managed vnetId) -> {| id = vnetId.Eval() |}
                                       | Some (Unmanaged vnetId) -> {| id = vnetId.Eval() |}
                                       | None -> Unchecked.defaultof<_>
                                |}
                            |}
                        )
                    |}
            |} :> _
