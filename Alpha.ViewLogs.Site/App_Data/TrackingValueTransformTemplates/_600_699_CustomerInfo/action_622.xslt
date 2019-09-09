<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />
  <xsl:output method="html" indent="no" />

  <xsl:template match="age_customerupdnicknamelogonly">
    <table class="tblDesc">
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'nicknameonly'" />
        <xsl:with-param name="firstValue" select="@nicknameonly" />

        <xsl:with-param name="secondKey" select="'ErrCode'" />
        <xsl:with-param name="secondValue" select="@errcode" />

        <xsl:with-param name="thirdKey" select="'ErrMsg'" />
        <xsl:with-param name="thirdValue" select="@errmsg" />
      </xsl:call-template>
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'IP'" />
        <xsl:with-param name="firstValue" select="xslLogFormat:FormatIP(@ip)" />
      </xsl:call-template>
    </table>
  </xsl:template>
</xsl:stylesheet>
