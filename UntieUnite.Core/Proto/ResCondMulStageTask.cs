// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Proto/res_CondMulStageTask.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace PbProtocol {

  /// <summary>Holder for reflection information generated from Proto/res_CondMulStageTask.proto</summary>
  public static partial class ResCondMulStageTaskReflection {

    #region Descriptor
    /// <summary>File descriptor for Proto/res_CondMulStageTask.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ResCondMulStageTaskReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiBQcm90by9yZXNfQ29uZE11bFN0YWdlVGFzay5wcm90bxILcGJfUHJvdG9j",
            "b2wiTAoHcGJTdGFnZRIUCgpNYXhQcm9jZXNzGIXnASABKAUSFAoKQXdhcmRS",
            "ZXNJRBjWuQEgASgFEhUKC0F3YXJkUmVzTnVtGMqqASABKAUiqgEKEnBiQ29u",
            "ZE11bFN0YWdlVGFzaxIQCgZUYXNrSUQY9q4BIAEoBRIWCgxNYXhEb25lQ291",
            "bnQYlaIBIAEoBRISCghUYXNrTmFtZRiq5wEgASgJEhIKCFRhc2tEZXNjGKi/",
            "ASABKAkSGwoRRmluaXNoQ29uZGl0aW9uSUQYqqwBIAEoBRIlCgVTdGFnZRjh",
            "rQEgAygLMhQucGJfUHJvdG9jb2wucGJTdGFnZSqOAQouUEJfTUFDUk9fTUFY",
            "X0NPTkRNVUxTVEFHRVRBU0tfVEFTS05BTUVfU1RSX0xFThIuCipQQl9NQVhf",
            "Q09ORE1VTFNUQUdFVEFTS19UQVNLTkFNRV9TVFJfTEVOXzAQABIsCihQQl9N",
            "QVhfQ09ORE1VTFNUQUdFVEFTS19UQVNLTkFNRV9TVFJfTEVOECAqjgEKLlBC",
            "X01BQ1JPX01BWF9DT05ETVVMU1RBR0VUQVNLX1RBU0tERVNDX1NUUl9MRU4S",
            "LgoqUEJfTUFYX0NPTkRNVUxTVEFHRVRBU0tfVEFTS0RFU0NfU1RSX0xFTl8w",
            "EAASLAooUEJfTUFYX0NPTkRNVUxTVEFHRVRBU0tfVEFTS0RFU0NfU1RSX0xF",
            "ThAgKoUBCitQQl9NQUNST19NQVhfQ09ORE1VTFNUQUdFVEFTS19TVEFHRV9D",
            "RkdfTlVNEisKJ1BCX01BWF9DT05ETVVMU1RBR0VUQVNLX1NUQUdFX0NGR19O",
            "VU1fMBAAEikKJVBCX01BWF9DT05ETVVMU1RBR0VUQVNLX1NUQUdFX0NGR19O",
            "VU0QCmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::PbProtocol.PB_MACRO_MAX_CONDMULSTAGETASK_TASKNAME_STR_LEN), typeof(global::PbProtocol.PB_MACRO_MAX_CONDMULSTAGETASK_TASKDESC_STR_LEN), typeof(global::PbProtocol.PB_MACRO_MAX_CONDMULSTAGETASK_STAGE_CFG_NUM), }, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::PbProtocol.pbStage), global::PbProtocol.pbStage.Parser, new[]{ "MaxProcess", "AwardResID", "AwardResNum" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::PbProtocol.pbCondMulStageTask), global::PbProtocol.pbCondMulStageTask.Parser, new[]{ "TaskID", "MaxDoneCount", "TaskName", "TaskDesc", "FinishConditionID", "Stage" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum PB_MACRO_MAX_CONDMULSTAGETASK_TASKNAME_STR_LEN {
    [pbr::OriginalName("PB_MAX_CONDMULSTAGETASK_TASKNAME_STR_LEN_0")] PbMaxCondmulstagetaskTasknameStrLen0 = 0,
    [pbr::OriginalName("PB_MAX_CONDMULSTAGETASK_TASKNAME_STR_LEN")] PbMaxCondmulstagetaskTasknameStrLen = 32,
  }

  public enum PB_MACRO_MAX_CONDMULSTAGETASK_TASKDESC_STR_LEN {
    [pbr::OriginalName("PB_MAX_CONDMULSTAGETASK_TASKDESC_STR_LEN_0")] PbMaxCondmulstagetaskTaskdescStrLen0 = 0,
    [pbr::OriginalName("PB_MAX_CONDMULSTAGETASK_TASKDESC_STR_LEN")] PbMaxCondmulstagetaskTaskdescStrLen = 32,
  }

  public enum PB_MACRO_MAX_CONDMULSTAGETASK_STAGE_CFG_NUM {
    [pbr::OriginalName("PB_MAX_CONDMULSTAGETASK_STAGE_CFG_NUM_0")] PbMaxCondmulstagetaskStageCfgNum0 = 0,
    [pbr::OriginalName("PB_MAX_CONDMULSTAGETASK_STAGE_CFG_NUM")] PbMaxCondmulstagetaskStageCfgNum = 10,
  }

  #endregion

  #region Messages
  public sealed partial class pbStage : pb::IMessage<pbStage> {
    private static readonly pb::MessageParser<pbStage> _parser = new pb::MessageParser<pbStage>(() => new pbStage());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<pbStage> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PbProtocol.ResCondMulStageTaskReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbStage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbStage(pbStage other) : this() {
      maxProcess_ = other.maxProcess_;
      awardResID_ = other.awardResID_;
      awardResNum_ = other.awardResNum_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbStage Clone() {
      return new pbStage(this);
    }

    /// <summary>Field number for the "MaxProcess" field.</summary>
    public const int MaxProcessFieldNumber = 29573;
    private int maxProcess_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int MaxProcess {
      get { return maxProcess_; }
      set {
        maxProcess_ = value;
      }
    }

    /// <summary>Field number for the "AwardResID" field.</summary>
    public const int AwardResIDFieldNumber = 23766;
    private int awardResID_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int AwardResID {
      get { return awardResID_; }
      set {
        awardResID_ = value;
      }
    }

    /// <summary>Field number for the "AwardResNum" field.</summary>
    public const int AwardResNumFieldNumber = 21834;
    private int awardResNum_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int AwardResNum {
      get { return awardResNum_; }
      set {
        awardResNum_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as pbStage);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(pbStage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (MaxProcess != other.MaxProcess) return false;
      if (AwardResID != other.AwardResID) return false;
      if (AwardResNum != other.AwardResNum) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (MaxProcess != 0) hash ^= MaxProcess.GetHashCode();
      if (AwardResID != 0) hash ^= AwardResID.GetHashCode();
      if (AwardResNum != 0) hash ^= AwardResNum.GetHashCode();
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
      if (AwardResNum != 0) {
        output.WriteRawTag(208, 212, 10);
        output.WriteInt32(AwardResNum);
      }
      if (AwardResID != 0) {
        output.WriteRawTag(176, 205, 11);
        output.WriteInt32(AwardResID);
      }
      if (MaxProcess != 0) {
        output.WriteRawTag(168, 184, 14);
        output.WriteInt32(MaxProcess);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (MaxProcess != 0) {
        size += 3 + pb::CodedOutputStream.ComputeInt32Size(MaxProcess);
      }
      if (AwardResID != 0) {
        size += 3 + pb::CodedOutputStream.ComputeInt32Size(AwardResID);
      }
      if (AwardResNum != 0) {
        size += 3 + pb::CodedOutputStream.ComputeInt32Size(AwardResNum);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pbStage other) {
      if (other == null) {
        return;
      }
      if (other.MaxProcess != 0) {
        MaxProcess = other.MaxProcess;
      }
      if (other.AwardResID != 0) {
        AwardResID = other.AwardResID;
      }
      if (other.AwardResNum != 0) {
        AwardResNum = other.AwardResNum;
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
          case 174672: {
            AwardResNum = input.ReadInt32();
            break;
          }
          case 190128: {
            AwardResID = input.ReadInt32();
            break;
          }
          case 236584: {
            MaxProcess = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class pbCondMulStageTask : pb::IMessage<pbCondMulStageTask> {
    private static readonly pb::MessageParser<pbCondMulStageTask> _parser = new pb::MessageParser<pbCondMulStageTask>(() => new pbCondMulStageTask());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<pbCondMulStageTask> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PbProtocol.ResCondMulStageTaskReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbCondMulStageTask() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbCondMulStageTask(pbCondMulStageTask other) : this() {
      taskID_ = other.taskID_;
      maxDoneCount_ = other.maxDoneCount_;
      taskName_ = other.taskName_;
      taskDesc_ = other.taskDesc_;
      finishConditionID_ = other.finishConditionID_;
      stage_ = other.stage_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbCondMulStageTask Clone() {
      return new pbCondMulStageTask(this);
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

    /// <summary>Field number for the "MaxDoneCount" field.</summary>
    public const int MaxDoneCountFieldNumber = 20757;
    private int maxDoneCount_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int MaxDoneCount {
      get { return maxDoneCount_; }
      set {
        maxDoneCount_ = value;
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

    /// <summary>Field number for the "FinishConditionID" field.</summary>
    public const int FinishConditionIDFieldNumber = 22058;
    private int finishConditionID_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int FinishConditionID {
      get { return finishConditionID_; }
      set {
        finishConditionID_ = value;
      }
    }

    /// <summary>Field number for the "Stage" field.</summary>
    public const int StageFieldNumber = 22241;
    private static readonly pb::FieldCodec<global::PbProtocol.pbStage> _repeated_stage_codec
        = pb::FieldCodec.ForMessage(177930, global::PbProtocol.pbStage.Parser);
    private readonly pbc::RepeatedField<global::PbProtocol.pbStage> stage_ = new pbc::RepeatedField<global::PbProtocol.pbStage>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::PbProtocol.pbStage> Stage {
      get { return stage_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as pbCondMulStageTask);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(pbCondMulStageTask other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (TaskID != other.TaskID) return false;
      if (MaxDoneCount != other.MaxDoneCount) return false;
      if (TaskName != other.TaskName) return false;
      if (TaskDesc != other.TaskDesc) return false;
      if (FinishConditionID != other.FinishConditionID) return false;
      if(!stage_.Equals(other.stage_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (TaskID != 0) hash ^= TaskID.GetHashCode();
      if (MaxDoneCount != 0) hash ^= MaxDoneCount.GetHashCode();
      if (TaskName.Length != 0) hash ^= TaskName.GetHashCode();
      if (TaskDesc.Length != 0) hash ^= TaskDesc.GetHashCode();
      if (FinishConditionID != 0) hash ^= FinishConditionID.GetHashCode();
      hash ^= stage_.GetHashCode();
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
      if (MaxDoneCount != 0) {
        output.WriteRawTag(168, 145, 10);
        output.WriteInt32(MaxDoneCount);
      }
      if (FinishConditionID != 0) {
        output.WriteRawTag(208, 226, 10);
        output.WriteInt32(FinishConditionID);
      }
      stage_.WriteTo(output, _repeated_stage_codec);
      if (TaskID != 0) {
        output.WriteRawTag(176, 247, 10);
        output.WriteInt32(TaskID);
      }
      if (TaskDesc.Length != 0) {
        output.WriteRawTag(194, 250, 11);
        output.WriteString(TaskDesc);
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
      if (MaxDoneCount != 0) {
        size += 3 + pb::CodedOutputStream.ComputeInt32Size(MaxDoneCount);
      }
      if (TaskName.Length != 0) {
        size += 3 + pb::CodedOutputStream.ComputeStringSize(TaskName);
      }
      if (TaskDesc.Length != 0) {
        size += 3 + pb::CodedOutputStream.ComputeStringSize(TaskDesc);
      }
      if (FinishConditionID != 0) {
        size += 3 + pb::CodedOutputStream.ComputeInt32Size(FinishConditionID);
      }
      size += stage_.CalculateSize(_repeated_stage_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pbCondMulStageTask other) {
      if (other == null) {
        return;
      }
      if (other.TaskID != 0) {
        TaskID = other.TaskID;
      }
      if (other.MaxDoneCount != 0) {
        MaxDoneCount = other.MaxDoneCount;
      }
      if (other.TaskName.Length != 0) {
        TaskName = other.TaskName;
      }
      if (other.TaskDesc.Length != 0) {
        TaskDesc = other.TaskDesc;
      }
      if (other.FinishConditionID != 0) {
        FinishConditionID = other.FinishConditionID;
      }
      stage_.Add(other.stage_);
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
          case 166056: {
            MaxDoneCount = input.ReadInt32();
            break;
          }
          case 176464: {
            FinishConditionID = input.ReadInt32();
            break;
          }
          case 177930: {
            stage_.AddEntriesFrom(input, _repeated_stage_codec);
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
