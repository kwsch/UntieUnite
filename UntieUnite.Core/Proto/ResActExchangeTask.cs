// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Proto/res_ActExchangeTask.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace PbProtocol {

  /// <summary>Holder for reflection information generated from Proto/res_ActExchangeTask.proto</summary>
  public static partial class ResActExchangeTaskReflection {

    #region Descriptor
    /// <summary>File descriptor for Proto/res_ActExchangeTask.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ResActExchangeTaskReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ch9Qcm90by9yZXNfQWN0RXhjaGFuZ2VUYXNrLnByb3RvEgtwYl9Qcm90b2Nv",
            "bCIuCg9wYkFjdENvbnN1bWVSZXMSDAoCSUQYg+QBIAEoBRINCgNOdW0Y09AB",
            "IAEoBSL/AQoRcGJBY3RFeGNoYW5nZVRhc2sSEAoGVGFza0lEGPauASABKAUS",
            "DwoFU2VxSUQYva4BIAEoBRIaChBFeGNoYW5nZU1heENvdW50GOjCASABKAUS",
            "EgoIVGFza05hbWUYqucBIAEoCRISCghUYXNrRGVzYxiovwEgASgJEhoKEEV4",
            "Y2hhbmdlVGFyZ2V0SUQYn8QBIAEoBRIbChFFeGNoYW5nZVRhcmdldE51bRj/",
            "1QEgASgFEjUKDUFjdENvbnN1bWVSZXMYr6cBIAMoCzIcLnBiX1Byb3RvY29s",
            "LnBiQWN0Q29uc3VtZVJlcxITCglSZXNldFR5cGUY5K4BIAEoBSqLAQotUEJf",
            "TUFDUk9fTUFYX0FDVEVYQ0hBTkdFVEFTS19UQVNLTkFNRV9TVFJfTEVOEi0K",
            "KVBCX01BWF9BQ1RFWENIQU5HRVRBU0tfVEFTS05BTUVfU1RSX0xFTl8wEAAS",
            "KwonUEJfTUFYX0FDVEVYQ0hBTkdFVEFTS19UQVNLTkFNRV9TVFJfTEVOECAq",
            "iwEKLVBCX01BQ1JPX01BWF9BQ1RFWENIQU5HRVRBU0tfVEFTS0RFU0NfU1RS",
            "X0xFThItCilQQl9NQVhfQUNURVhDSEFOR0VUQVNLX1RBU0tERVNDX1NUUl9M",
            "RU5fMBAAEisKJ1BCX01BWF9BQ1RFWENIQU5HRVRBU0tfVEFTS0RFU0NfU1RS",
            "X0xFThAgKpoBCjJQQl9NQUNST19NQVhfQUNURVhDSEFOR0VUQVNLX0FDVENP",
            "TlNVTUVSRVNfQ0ZHX05VTRIyCi5QQl9NQVhfQUNURVhDSEFOR0VUQVNLX0FD",
            "VENPTlNVTUVSRVNfQ0ZHX05VTV8wEAASMAosUEJfTUFYX0FDVEVYQ0hBTkdF",
            "VEFTS19BQ1RDT05TVU1FUkVTX0NGR19OVU0QA2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::PbProtocol.PB_MACRO_MAX_ACTEXCHANGETASK_TASKNAME_STR_LEN), typeof(global::PbProtocol.PB_MACRO_MAX_ACTEXCHANGETASK_TASKDESC_STR_LEN), typeof(global::PbProtocol.PB_MACRO_MAX_ACTEXCHANGETASK_ACTCONSUMERES_CFG_NUM), }, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::PbProtocol.pbActConsumeRes), global::PbProtocol.pbActConsumeRes.Parser, new[]{ "ID", "Num" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::PbProtocol.pbActExchangeTask), global::PbProtocol.pbActExchangeTask.Parser, new[]{ "TaskID", "SeqID", "ExchangeMaxCount", "TaskName", "TaskDesc", "ExchangeTargetID", "ExchangeTargetNum", "ActConsumeRes", "ResetType" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum PB_MACRO_MAX_ACTEXCHANGETASK_TASKNAME_STR_LEN {
    [pbr::OriginalName("PB_MAX_ACTEXCHANGETASK_TASKNAME_STR_LEN_0")] PbMaxActexchangetaskTasknameStrLen0 = 0,
    [pbr::OriginalName("PB_MAX_ACTEXCHANGETASK_TASKNAME_STR_LEN")] PbMaxActexchangetaskTasknameStrLen = 32,
  }

  public enum PB_MACRO_MAX_ACTEXCHANGETASK_TASKDESC_STR_LEN {
    [pbr::OriginalName("PB_MAX_ACTEXCHANGETASK_TASKDESC_STR_LEN_0")] PbMaxActexchangetaskTaskdescStrLen0 = 0,
    [pbr::OriginalName("PB_MAX_ACTEXCHANGETASK_TASKDESC_STR_LEN")] PbMaxActexchangetaskTaskdescStrLen = 32,
  }

  public enum PB_MACRO_MAX_ACTEXCHANGETASK_ACTCONSUMERES_CFG_NUM {
    [pbr::OriginalName("PB_MAX_ACTEXCHANGETASK_ACTCONSUMERES_CFG_NUM_0")] PbMaxActexchangetaskActconsumeresCfgNum0 = 0,
    [pbr::OriginalName("PB_MAX_ACTEXCHANGETASK_ACTCONSUMERES_CFG_NUM")] PbMaxActexchangetaskActconsumeresCfgNum = 3,
  }

  #endregion

  #region Messages
  public sealed partial class pbActConsumeRes : pb::IMessage<pbActConsumeRes> {
    private static readonly pb::MessageParser<pbActConsumeRes> _parser = new pb::MessageParser<pbActConsumeRes>(() => new pbActConsumeRes());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<pbActConsumeRes> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PbProtocol.ResActExchangeTaskReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbActConsumeRes() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbActConsumeRes(pbActConsumeRes other) : this() {
      iD_ = other.iD_;
      num_ = other.num_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbActConsumeRes Clone() {
      return new pbActConsumeRes(this);
    }

    /// <summary>Field number for the "ID" field.</summary>
    public const int IDFieldNumber = 29187;
    private int iD_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ID {
      get { return iD_; }
      set {
        iD_ = value;
      }
    }

    /// <summary>Field number for the "Num" field.</summary>
    public const int NumFieldNumber = 26707;
    private int num_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Num {
      get { return num_; }
      set {
        num_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as pbActConsumeRes);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(pbActConsumeRes other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ID != other.ID) return false;
      if (Num != other.Num) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (ID != 0) hash ^= ID.GetHashCode();
      if (Num != 0) hash ^= Num.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Num != 0) {
        output.WriteRawTag(152, 133, 13);
        output.WriteInt32(Num);
      }
      if (ID != 0) {
        output.WriteRawTag(152, 160, 14);
        output.WriteInt32(ID);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ID != 0) {
        size += 3 + pb::CodedOutputStream.ComputeInt32Size(ID);
      }
      if (Num != 0) {
        size += 3 + pb::CodedOutputStream.ComputeInt32Size(Num);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pbActConsumeRes other) {
      if (other == null) {
        return;
      }
      if (other.ID != 0) {
        ID = other.ID;
      }
      if (other.Num != 0) {
        Num = other.Num;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 213656: {
            Num = input.ReadInt32();
            break;
          }
          case 233496: {
            ID = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class pbActExchangeTask : pb::IMessage<pbActExchangeTask> {
    private static readonly pb::MessageParser<pbActExchangeTask> _parser = new pb::MessageParser<pbActExchangeTask>(() => new pbActExchangeTask());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<pbActExchangeTask> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PbProtocol.ResActExchangeTaskReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbActExchangeTask() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbActExchangeTask(pbActExchangeTask other) : this() {
      taskID_ = other.taskID_;
      seqID_ = other.seqID_;
      exchangeMaxCount_ = other.exchangeMaxCount_;
      taskName_ = other.taskName_;
      taskDesc_ = other.taskDesc_;
      exchangeTargetID_ = other.exchangeTargetID_;
      exchangeTargetNum_ = other.exchangeTargetNum_;
      actConsumeRes_ = other.actConsumeRes_.Clone();
      resetType_ = other.resetType_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbActExchangeTask Clone() {
      return new pbActExchangeTask(this);
    }

    /// <summary>Field number for the "TaskID" field.</summary>
    public const int TaskIDFieldNumber = 22390;
    private int taskID_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int TaskID {
      get { return taskID_; }
      set {
        taskID_ = value;
      }
    }

    /// <summary>Field number for the "SeqID" field.</summary>
    public const int SeqIDFieldNumber = 22333;
    private int seqID_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int SeqID {
      get { return seqID_; }
      set {
        seqID_ = value;
      }
    }

    /// <summary>Field number for the "ExchangeMaxCount" field.</summary>
    public const int ExchangeMaxCountFieldNumber = 24936;
    private int exchangeMaxCount_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ExchangeMaxCount {
      get { return exchangeMaxCount_; }
      set {
        exchangeMaxCount_ = value;
      }
    }

    /// <summary>Field number for the "TaskName" field.</summary>
    public const int TaskNameFieldNumber = 29610;
    private string taskName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string TaskName {
      get { return taskName_; }
      set {
        taskName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "TaskDesc" field.</summary>
    public const int TaskDescFieldNumber = 24488;
    private string taskDesc_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string TaskDesc {
      get { return taskDesc_; }
      set {
        taskDesc_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "ExchangeTargetID" field.</summary>
    public const int ExchangeTargetIDFieldNumber = 25119;
    private int exchangeTargetID_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ExchangeTargetID {
      get { return exchangeTargetID_; }
      set {
        exchangeTargetID_ = value;
      }
    }

    /// <summary>Field number for the "ExchangeTargetNum" field.</summary>
    public const int ExchangeTargetNumFieldNumber = 27391;
    private int exchangeTargetNum_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ExchangeTargetNum {
      get { return exchangeTargetNum_; }
      set {
        exchangeTargetNum_ = value;
      }
    }

    /// <summary>Field number for the "ActConsumeRes" field.</summary>
    public const int ActConsumeResFieldNumber = 21423;
    private static readonly pb::FieldCodec<global::PbProtocol.pbActConsumeRes> _repeated_actConsumeRes_codec
        = pb::FieldCodec.ForMessage(171386, global::PbProtocol.pbActConsumeRes.Parser);
    private readonly pbc::RepeatedField<global::PbProtocol.pbActConsumeRes> actConsumeRes_ = new pbc::RepeatedField<global::PbProtocol.pbActConsumeRes>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::PbProtocol.pbActConsumeRes> ActConsumeRes {
      get { return actConsumeRes_; }
    }

    /// <summary>Field number for the "ResetType" field.</summary>
    public const int ResetTypeFieldNumber = 22372;
    private int resetType_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ResetType {
      get { return resetType_; }
      set {
        resetType_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as pbActExchangeTask);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(pbActExchangeTask other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (TaskID != other.TaskID) return false;
      if (SeqID != other.SeqID) return false;
      if (ExchangeMaxCount != other.ExchangeMaxCount) return false;
      if (TaskName != other.TaskName) return false;
      if (TaskDesc != other.TaskDesc) return false;
      if (ExchangeTargetID != other.ExchangeTargetID) return false;
      if (ExchangeTargetNum != other.ExchangeTargetNum) return false;
      if(!actConsumeRes_.Equals(other.actConsumeRes_)) return false;
      if (ResetType != other.ResetType) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (TaskID != 0) hash ^= TaskID.GetHashCode();
      if (SeqID != 0) hash ^= SeqID.GetHashCode();
      if (ExchangeMaxCount != 0) hash ^= ExchangeMaxCount.GetHashCode();
      if (TaskName.Length != 0) hash ^= TaskName.GetHashCode();
      if (TaskDesc.Length != 0) hash ^= TaskDesc.GetHashCode();
      if (ExchangeTargetID != 0) hash ^= ExchangeTargetID.GetHashCode();
      if (ExchangeTargetNum != 0) hash ^= ExchangeTargetNum.GetHashCode();
      hash ^= actConsumeRes_.GetHashCode();
      if (ResetType != 0) hash ^= ResetType.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      actConsumeRes_.WriteTo(output, _repeated_actConsumeRes_codec);
      if (SeqID != 0) {
        output.WriteRawTag(232, 243, 10);
        output.WriteInt32(SeqID);
      }
      if (ResetType != 0) {
        output.WriteRawTag(160, 246, 10);
        output.WriteInt32(ResetType);
      }
      if (TaskID != 0) {
        output.WriteRawTag(176, 247, 10);
        output.WriteInt32(TaskID);
      }
      if (TaskDesc.Length != 0) {
        output.WriteRawTag(194, 250, 11);
        output.WriteString(TaskDesc);
      }
      if (ExchangeMaxCount != 0) {
        output.WriteRawTag(192, 150, 12);
        output.WriteInt32(ExchangeMaxCount);
      }
      if (ExchangeTargetID != 0) {
        output.WriteRawTag(248, 161, 12);
        output.WriteInt32(ExchangeTargetID);
      }
      if (ExchangeTargetNum != 0) {
        output.WriteRawTag(248, 175, 13);
        output.WriteInt32(ExchangeTargetNum);
      }
      if (TaskName.Length != 0) {
        output.WriteRawTag(210, 186, 14);
        output.WriteString(TaskName);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (TaskID != 0) {
        size += 3 + pb::CodedOutputStream.ComputeInt32Size(TaskID);
      }
      if (SeqID != 0) {
        size += 3 + pb::CodedOutputStream.ComputeInt32Size(SeqID);
      }
      if (ExchangeMaxCount != 0) {
        size += 3 + pb::CodedOutputStream.ComputeInt32Size(ExchangeMaxCount);
      }
      if (TaskName.Length != 0) {
        size += 3 + pb::CodedOutputStream.ComputeStringSize(TaskName);
      }
      if (TaskDesc.Length != 0) {
        size += 3 + pb::CodedOutputStream.ComputeStringSize(TaskDesc);
      }
      if (ExchangeTargetID != 0) {
        size += 3 + pb::CodedOutputStream.ComputeInt32Size(ExchangeTargetID);
      }
      if (ExchangeTargetNum != 0) {
        size += 3 + pb::CodedOutputStream.ComputeInt32Size(ExchangeTargetNum);
      }
      size += actConsumeRes_.CalculateSize(_repeated_actConsumeRes_codec);
      if (ResetType != 0) {
        size += 3 + pb::CodedOutputStream.ComputeInt32Size(ResetType);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pbActExchangeTask other) {
      if (other == null) {
        return;
      }
      if (other.TaskID != 0) {
        TaskID = other.TaskID;
      }
      if (other.SeqID != 0) {
        SeqID = other.SeqID;
      }
      if (other.ExchangeMaxCount != 0) {
        ExchangeMaxCount = other.ExchangeMaxCount;
      }
      if (other.TaskName.Length != 0) {
        TaskName = other.TaskName;
      }
      if (other.TaskDesc.Length != 0) {
        TaskDesc = other.TaskDesc;
      }
      if (other.ExchangeTargetID != 0) {
        ExchangeTargetID = other.ExchangeTargetID;
      }
      if (other.ExchangeTargetNum != 0) {
        ExchangeTargetNum = other.ExchangeTargetNum;
      }
      actConsumeRes_.Add(other.actConsumeRes_);
      if (other.ResetType != 0) {
        ResetType = other.ResetType;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 171386: {
            actConsumeRes_.AddEntriesFrom(input, _repeated_actConsumeRes_codec);
            break;
          }
          case 178664: {
            SeqID = input.ReadInt32();
            break;
          }
          case 178976: {
            ResetType = input.ReadInt32();
            break;
          }
          case 179120: {
            TaskID = input.ReadInt32();
            break;
          }
          case 195906: {
            TaskDesc = input.ReadString();
            break;
          }
          case 199488: {
            ExchangeMaxCount = input.ReadInt32();
            break;
          }
          case 200952: {
            ExchangeTargetID = input.ReadInt32();
            break;
          }
          case 219128: {
            ExchangeTargetNum = input.ReadInt32();
            break;
          }
          case 236882: {
            TaskName = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
